using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCurrentShoppingCart();

        Task SuccessPurchase();

        Task FailurePurchase();

        Task ResetShoppingCart();
    }
}