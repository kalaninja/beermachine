using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sibintek.BeerMachine.DataContracts;
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

        private readonly IShoppingCartService _shoppingCartService;
        
        public PayController(
            IHubContext<CartHub, ICartHub> cartHubContext,
            IShoppingCartService shoppingCartService)
        {
            _cartHubContext = cartHubContext;
            _shoppingCartService = shoppingCartService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Index([FromBody] Account account)
        {
            var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();

            var purchaseResult = new PurchaseResult
            {
                Success = true,
                ShoppingCart = shoppingCart,
                WalletBalance = 100,
                Customer = "Иванов Иван"
            };

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