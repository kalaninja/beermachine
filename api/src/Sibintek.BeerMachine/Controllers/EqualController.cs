using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.BlockchainClient;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EqualController : ControllerBase
    {
        private readonly IBlockсhainClient _blockсhainClient;

        public EqualController(IBlockсhainClient blockсhainClient)
        {
            _blockсhainClient = blockсhainClient;
        }

        [HttpGet]
        public JsonResult Index()
        {
//            long[] ids = {302, 349};
//
//            foreach (var id in ids)
//            {
//                for (var i = 0; i < 300; i++)
//                {
//                    await _blockсhainClient.Issue(id);
//                }
//            }

//            const int sum = 167;
//            var report = await _blockсhainClient.Report(500);
//
//            report.TopRich
//                .Where(x => x.Balance < sum)
//                .ToList()
//                .ForEach(x =>
//                {
//                    var diff = sum - x.Balance;
//                    for (var i = 0; i < diff; i++)
//                    {
//                        _blockсhainClient.Issue(x.Id);
//                    }
//                });

            return new JsonResult("true");
        }
    }
}