using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.BlockchainClient;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBlockсhainClient _blockсhainClient;

        public BalanceController(IBlockсhainClient blockсhainClient)
        {
            _blockсhainClient = blockсhainClient;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Index([FromBody] Account account)
        {
            var wallet = await _blockсhainClient.GetWallet(account.Id);
            return wallet == null 
                ? Result.NotFound() 
                : Result.Ok(wallet.Balance);
        }

        [HttpPost("mock")]
        public ActionResult<Result> Mock([FromBody] Account account)
        {
            return Result.Ok(5);
        }
    }
}