using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.SignalrHubs
{
    public interface ICartHub
    {
        Task UpdateShoppingCart(ShoppingCart shoppingCart);

        Task UpdatePurchaseResult(PurchaseResult purchaseResult);
    }
}