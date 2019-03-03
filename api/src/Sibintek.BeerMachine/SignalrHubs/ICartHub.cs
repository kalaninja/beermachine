using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.SignalrHubs
{
    public interface ICartHub
    {
        Task UpdateShoppingCart(ShoppingCart shoppingCart);
    }
}