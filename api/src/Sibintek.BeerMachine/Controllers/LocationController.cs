using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public LocationController(IWalletService walletService, ILoggerFactory loggerFactory)
        {
            _walletService = walletService;
            _logger = loggerFactory.CreateLogger<LocationController>();
        }

        [HttpPost]
        public ActionResult Index([FromBody] Location[] locations)
        {
            _logger.LogDebug(
                "Location: " + string.Join(",", locations.Select(x => $"{x.Id}:{x.Room}")));

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