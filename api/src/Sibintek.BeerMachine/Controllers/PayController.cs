using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PayController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Result> Index([FromBody] Person person)
        {
            return Result.Ok(5);
        }
    }
}