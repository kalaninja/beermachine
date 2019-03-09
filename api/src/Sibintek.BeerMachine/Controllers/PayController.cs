using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Services;
using Sibintek.BeerMachine.SignalrHubs;

namespace Sibintek.BeerMachine.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IHubContext<CartHub, ICartHub> _cartHubContext;

        private readonly IPurchaseService _purchaseService;

        public PayController(
            IHubContext<CartHub, ICartHub> cartHubContext, IPurchaseService purchaseService)
        {
            _cartHubContext = cartHubContext;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<ActionResult> Index([FromBody] Account account)
        {
            var purchaseResult = await _purchaseService.MakePurchase(account);

            await _cartHubContext.Clients.All.UpdatePurchaseResult(purchaseResult);

            return Ok();
        }

        [HttpPost("mock")]
        public ActionResult Mock([FromBody] Account account)
        {
            return Ok();
        }
    }
}