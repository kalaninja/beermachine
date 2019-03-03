using System.Linq;
using System.Threading.Tasks;
using Sibintek.BeerMachine.Services;
using Sibintek.BeerMachine.Settings;
using Xunit;

namespace Sibintek.BeerMachine.Tests
{
    public class ShoppingCartServiceTests
    {
        [Theory]
        [InlineData("http://35.228.0.95/mock")]
        public async Task GetCurrentShoppingCartTest(string serviceUrl)
        {
            var options = new ShoppingCartServiceOptions
            {
                ServiceUrl = serviceUrl
            };

            var shoppingCartService = new ShoppingCartService(options);

            var shoppingCart = await shoppingCartService.GetCurrentShoppingCart();
            
            Assert.NotNull(shoppingCart);
            Assert.True(
                shoppingCart.Total  == (shoppingCart?.Products.Sum(x=>x.Total) ?? 0),
                "Total of shopping cart not equal to products total");
        }
    }
}