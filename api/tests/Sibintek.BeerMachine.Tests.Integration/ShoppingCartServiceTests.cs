using System.Linq;
using System.Threading.Tasks;
using Shouldly;
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

            shoppingCart.ShouldNotBeNull();
            shoppingCart.Items.ShouldNotBeNull();
            shoppingCart.Items.Count.ShouldBe(4);
            shoppingCart.Items.Distinct().Count().ShouldBe(4);
            shoppingCart.Items.All(x => x.Count > 0 && x.Total > 0 && !string.IsNullOrEmpty(x.Name)).ShouldBeTrue();
        }
    }
}