using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public HomeController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Cart()
        {
            var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();

            return Json(shoppingCart);
        }
    }
}