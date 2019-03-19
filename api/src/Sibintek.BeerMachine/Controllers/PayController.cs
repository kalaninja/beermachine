using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Services;
using Sibintek.BeerMachine.Settings;
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

        private readonly IMaintenanceService _maintenanceService;

        private readonly MaintenanceOptions _maintenanceOptions;

        private readonly ILogger _logger;

        private static readonly ConcurrentDictionary<long, DateTime> AccessTimes =
            new ConcurrentDictionary<long, DateTime>();

        public PayController(
            IHubContext<CartHub, ICartHub> cartHubContext,
            IPurchaseService purchaseService,
            IMaintenanceService maintenanceService,
            MaintenanceOptions maintenanceOptions,
            ILoggerFactory loggerFactory)
        {
            _cartHubContext = cartHubContext;
            _purchaseService = purchaseService;
            _maintenanceService = maintenanceService;
            _maintenanceOptions = maintenanceOptions;
            _logger = loggerFactory.CreateLogger<PayController>();
        }

        [HttpPost]
        public async Task<ActionResult> Index([FromBody] Account account)
        {
            _logger.LogDebug($"Pay: {account.Id}");

            var now = DateTime.Now;
            if (AccessTimes.TryGetValue(account.Id, out var accessTime) && now.Subtract(accessTime).TotalSeconds < 10)
            {
                return Ok();
            }

            AccessTimes.AddOrUpdate(account.Id, now, (_, value) => value);

            if (_maintenanceOptions.MasterKeys.Contains(account.Id))
            {
                await _maintenanceService.ResetShoppingCart();
            }
            else
            {
                var purchaseResult = await _purchaseService.MakePurchase(account);

                await _cartHubContext.Clients.All.UpdatePurchaseResult(purchaseResult);
            }

            return Ok();
        }

        [HttpPost("mock")]
        public ActionResult Mock([FromBody] Account account)
        {
            return Ok();
        }
    }
}