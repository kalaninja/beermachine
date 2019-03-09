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
        public async Task<ActionResult> Mock([FromBody] Account account, PurchaseStatus status = PurchaseStatus.Success, int errorType = 0)
        {
            var shoppingCart = new ShoppingCart(new List<ShoppingCart.Item>()
            {
                new ShoppingCart.Item("Борщ", 1, 1),
                new ShoppingCart.Item("Уха", 2, 2),
                new ShoppingCart.Item("Требуха", 1, 1),
            });
            
            var purchaseResult = new PurchaseResult
            {
                Status = status,
                ShoppingCart = shoppingCart,
                WalletBalance = 100,
                Customer = "Иванов Иван",
                TransactionHash = "3b8d3d62456a56e047eeb4b37dc9b6d27285cb63877c929b3a6aae574b6e4de2",
            };

            if (status == PurchaseStatus.Rejected)
            {
                purchaseResult.ErrorDescription = "Недостаточно средств";
            }

            if (status == PurchaseStatus.Error)
            {
                if (errorType == 0)
                {
                    purchaseResult.ErrorDescription = "Не найден кошелек покупателя";
                    purchaseResult.ShoppingCart = null;
                }
                else
                {
                    purchaseResult.ErrorDescription = "Что-то пошло не так. Обратитесь за помощью к администратору системы.";
                    purchaseResult.ShoppingCart = null;
                    purchaseResult.ExceptionText = "Exception";
                }
            }
            
            await _cartHubContext.Clients.All.UpdatePurchaseResult(purchaseResult);
            
            
            return Ok();
        }
    }
}