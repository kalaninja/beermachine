using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IWalletService _walletService;
        
        private readonly IBlockсhainClient _blockсhainClient;

        public LocationController(IWalletService walletService, IBlockсhainClient blockсhainClient)
        {
            _walletService = walletService;
            _blockсhainClient = blockсhainClient;
        }
        
        [HttpPost]
        public ActionResult Index([FromBody] Location[] locations)
        {
            _walletService.UpdateWallets(locations);
            
            return Ok();
        }

        [HttpPost("mock")]
        public ActionResult Mock([FromBody] Location[] locations)
        {
            foreach (var location in locations)
            {
                _blockсhainClient.Issue(location.Id);
            }
            
            return Ok();
        }
    }
}