using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.BlockchainClient;

namespace Sibintek.BeerMachine.Controllers
{
    public class CashierController : Controller
    {
        private readonly IBlockсhainClient _blockсhainClient;

        public CashierController(IBlockсhainClient blockсhainClient)
        {
            _blockсhainClient = blockсhainClient;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(int wallet, int amount)
        {
            var wal = await _blockсhainClient.GetWallet(wallet);
            if (wal == null)
            {
                return View(model: $"Кошелек {wallet} не найден");
            }

            var transactionResponse = await _blockсhainClient.Pay(wallet, amount);
            if (transactionResponse == null)
            {
                return View(
                    model:
                    $"Статус транзакции неизвестен. Кошелек: {wallet}\nCумма: {amount}");
            }

            const int max = 10;
            for (var i = 0; i < max; i++)
            {
                var status = await _blockсhainClient.GetStatus(transactionResponse.TxHash);
                if (status.IsSuccess)
                {
                    return View(model:
                        $"Транзакция прошла успешно.\nHash: {transactionResponse.TxHash}\nОстаток: {wal.Balance}\nКошелек: {wallet}\nCумма: {amount}");
                }

                if (status.IsError)
                {
                    return View(model:
                        $"Транзакция отклонена.\nHash: {transactionResponse.TxHash}\nОстаток: {wal.Balance}\nКошелек: {wallet}\nCумма: {amount}");
                }

                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }

            return View(
                model:
                $"Статус транзакции неизвестен после {max} попыток. Hash: {transactionResponse.TxHash}\nКошелек: {wallet}\nCумма: {amount}");
        }
    }
}