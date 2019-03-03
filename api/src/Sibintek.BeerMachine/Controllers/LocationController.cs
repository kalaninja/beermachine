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

        public LocationController(IWalletService walletService)
        {
            _walletService = walletService;
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
            return Ok();
        }
    }
}