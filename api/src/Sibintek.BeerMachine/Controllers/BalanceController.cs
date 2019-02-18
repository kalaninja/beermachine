using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Result> Index([FromBody] Account account)
        {
            return Result.Ok(5);
        }

        [HttpPost("mock")]
        public ActionResult<Result> Mock([FromBody] Account account)
        {
            return Result.Ok(5);
        }
    }
}