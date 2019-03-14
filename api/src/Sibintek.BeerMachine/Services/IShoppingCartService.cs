using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCurrentShoppingCart();

        Task SuccessPurchase(PurchaseResult purchaseResult);

        Task FailurePurchase(PurchaseResult purchaseResult);

        Task ResetShoppingCart();
    }
}