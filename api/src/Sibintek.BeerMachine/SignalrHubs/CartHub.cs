using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.SignalrHubs
{
    public class CartHub: Hub<ICartHub>
    {
        public async Task UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            await Clients.All.UpdateShoppingCart(shoppingCart);
        }
    }
}