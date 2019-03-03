using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.SignalrHubs
{
    public class CartHub: Hub<ICartHub>
    {
        public async Task UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            await Clients.All.UpdateShoppingCart(shoppingCart);
        }

        public async Task UpdatePurchaseResult(PurchaseResult purchaseResult)
        {
            await Clients.All.UpdatePurchaseResult(purchaseResult);
        }
    }
}