using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MimeMapping;
using Newtonsoft.Json;
using Polly;
using Sibintek.BeerMachine.Settings;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoppingCartServiceOptions _shoppingCartServiceOptions;

        private readonly IHttpClientFactory _httpClientFactory;

        public ShoppingCartService(ShoppingCartServiceOptions shoppingCartServiceOptions,
            IHttpClientFactory httpClientFactory)
        {
            _shoppingCartServiceOptions = shoppingCartServiceOptions;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ShoppingCart> GetCurrentShoppingCart()
        {
            return await Policy
                .Handle<Exception>()
                .RetryAsync(2)
                .ExecuteAsync(async () =>
                {
                    using (var response = await _httpClientFactory.CreateClient()
                        .GetAsync(_shoppingCartServiceOptions.ServiceUrl))
                    {
                        var responseText = await response
                            .EnsureSuccessStatusCode()
                            .Content
                            .ReadAsStringAsync();

                        return Parse(responseText);
                    }
                });
        }

        public Task SuccessPurchase(PurchaseResult purchaseResult)
        {
            return Status("success", purchaseResult);
        }

        public Task FailurePurchase(PurchaseResult purchaseResult)
        {
            return Status("failure", purchaseResult);
        }

        public Task ResetShoppingCart()
        {
            return Status("reset", null);
        }

        private async Task Status(string status, PurchaseResult purchaseResult)
        {
            await Policy
                .Handle<Exception>()
                .RetryAsync(2)
                .ExecuteAsync(async () =>
                {
                    var body = JsonConvert.SerializeObject(new StatusRequest {status = status, data = purchaseResult});
                    var content = new StringContent(body, Encoding.UTF8, KnownMimeTypes.Json);

                    using (var response = await _httpClientFactory.CreateClient()
                        .PostAsync(_shoppingCartServiceOptions.StatusUrl, content))
                    {
                        response.EnsureSuccessStatusCode();
                    }
                });
        }

        private static ShoppingCart Parse(string responseText)
        {
            var response = JsonConvert.DeserializeObject<ShoppingCartResponse>(responseText);

            var items = response.products
                .Zip(response.quantity, (name, count) => (name, count))
                .Zip(response.price_for_each,
                    ((string name, int count) x, long price) => new ShoppingCart.Item(x.name, price, x.count))
                .ToList();

            return new ShoppingCart(items);
        }

        private class ShoppingCartResponse
        {
            public long[] price_for_each { get; set; }

            public string[] products { get; set; }

            public int[] quantity { get; set; }
        }

        private class StatusRequest
        {
            public string status { get; set; }

            public PurchaseResult data { get; set; }
        }
    }
}