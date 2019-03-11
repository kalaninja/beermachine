using System.Threading.Tasks;

namespace Sibintek.BeerMachine.Services
{
    public interface IMaintenanceService
    {
        Task ResetShoppingCart();
    }
}