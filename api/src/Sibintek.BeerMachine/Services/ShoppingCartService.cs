using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sibintek.BeerMachine.Settings;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoppingCartServiceOptions _shoppingCartServiceOptions;
        
        private readonly IHttpClientFactory _httpClientFactory;

        public ShoppingCartService(ShoppingCartServiceOptions shoppingCartServiceOptions, IHttpClientFactory httpClientFactory)
        {
            _shoppingCartServiceOptions = shoppingCartServiceOptions;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ShoppingCart> GetCurrentShoppingCart()
        {
            using (var response = await _httpClientFactory.CreateClient().GetAsync(_shoppingCartServiceOptions.ServiceUrl))
            {
                var responseText = await response
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync();

                return Parse(responseText);
            }
        }

        private static ShoppingCart Parse(string responseText)
        {
            var response = JsonConvert.DeserializeObject<ShoppingCartResponse>(responseText);

            var items = response.products
                .Zip(response.quantity, (name, count) => (name, count))
                .Zip(response.price_for_each,
                    ((string name, int count) x, decimal price) => new ShoppingCart.Item(x.name, price, x.count))
                .ToList();

            return new ShoppingCart(items);
        }
        
        private class ShoppingCartResponse
        {
            public decimal[] price_for_each { get; set; }
            
            public string[] products { get; set; }
            
            public int[] quantity { get; set; }
        }
    }
}