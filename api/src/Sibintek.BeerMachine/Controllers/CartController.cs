using Microsoft.AspNetCore.Mvc;

namespace Sibintek.BeerMachine.Controllers
{
    public class CartController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}