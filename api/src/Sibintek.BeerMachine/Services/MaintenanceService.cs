using System.Threading.Tasks;

namespace Sibintek.BeerMachine.Services
{
    public class MaintenanceService: IMaintenanceService
    {
        private readonly IShoppingCartService _shoppingCartService;

        public MaintenanceService(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        
        public Task ResetShoppingCart()
        {
            return _shoppingCartService.ResetShoppingCart();
        }
    }
}