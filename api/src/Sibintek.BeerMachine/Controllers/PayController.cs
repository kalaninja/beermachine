using System;
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
                Status = PurchaseStatus.Success,
                ShoppingCart = shoppingCart,
                WalletBalance = 100,
                Customer = "Иванов Иван"
            };

            await _cartHubContext.Clients.All.UpdatePurchaseResult(purchaseResult);
            
            return Ok();
        }

        [HttpPost("mock")]
        public async Task<ActionResult> Mock([FromBody] Account account, PurchaseStatus status = PurchaseStatus.Success, int errorType = 0)
        {
            var shoppingCart = new ShoppingCart(new List<ShoppingCart.Item>()
            {
                new ShoppingCart.Item("Борщ", 200, 1),
                new ShoppingCart.Item("Уха", 100, 2),
                new ShoppingCart.Item("Требуха", 300, 3),
            });
            
            var purchaseResult = new PurchaseResult
            {
                Status = status,
                ShoppingCart = shoppingCart,
                WalletBalance = 100,
                Customer = "Иванов Иван",
                TransactionHash = "2C886F84AD23BA59CF12EA15A80FBA5C",
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