using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IReportService _reportService;

        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Data()
        {
            var reportData = await _reportService.GetReport();

            return Json(reportData);
        }
    }
}