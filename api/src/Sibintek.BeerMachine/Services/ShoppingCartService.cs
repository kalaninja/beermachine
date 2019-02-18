using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sibintek.BeerMachine.Settings;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoppingCartServiceOptions _shoppingCartServiceOptions;

        public ShoppingCartService(ShoppingCartServiceOptions shoppingCartServiceOptions)
        {
            _shoppingCartServiceOptions = shoppingCartServiceOptions;
        }

        public async Task<ShoppingCart> GetCurrentShoppingCart()
        {
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(_shoppingCartServiceOptions.ServiceUrl))
            {
                var responseText = await response
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync();

                return Map(responseText);
            }
        }

        private static ShoppingCart Map(string responseText)
        {
            const short responseArrayLength = 4;

            var jsonArray = JArray.Parse(responseText);
            if (jsonArray.Count < responseArrayLength)
            {
                throw new Exception("wrong response from shopping cart service");
            }

            var productNames = jsonArray[0].ToObject<string[]>();
            var productCounts = jsonArray[1].ToObject<int[]>();
            var productPrices = jsonArray[2].ToObject<decimal[]>();
            var total = jsonArray[3].ToObject<decimal>();

            if (productNames.Length != productCounts.Length
                || productNames.Length != productPrices.Length)
            {
                throw new Exception("wrong array length in response from shopping cart service");
            }

            return new ShoppingCart
            {
                Products = productNames
                    .Zip(productCounts, (name, count) => (name, count))
                    .Zip(productPrices, (x, price) => new Product
                    {
                        Name = x.Item1,
                        Count = x.Item2,
                        Price = price
                    })
                    .ToList(),
                Total = total
            };
        }
    }
}