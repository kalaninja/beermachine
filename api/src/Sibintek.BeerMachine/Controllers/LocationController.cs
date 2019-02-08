using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        [HttpPost]
        public ActionResult Index([FromBody] Location[] locations)
        {
            return Ok();
        }
    }
}