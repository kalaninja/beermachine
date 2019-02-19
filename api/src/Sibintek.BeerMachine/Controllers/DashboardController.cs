using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sibintek.BeerMachine.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Cart()
        {
            return View();
        }
    }
}