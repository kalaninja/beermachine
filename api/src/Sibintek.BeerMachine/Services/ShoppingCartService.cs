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
        private readonly AppSettings _appSettings;

        public ShoppingCartService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<ShoppingCart> GetCurrentShoppingCart()
        {
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(_appSettings.ShoppingCartServiceUrl))
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
                    .Select((name, index) =>
                        new Product
                        {
                            Name = name,
                            Count = productCounts[index],
                            Price = productPrices[index]
                        })
                    .ToList(),
                Total = total
            };
        }
    }
}