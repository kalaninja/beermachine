using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sibintek.BeerMachine.BlockchainClient;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IShoppingCartService _shoppingCartService;

        private readonly IBlockсhainClient _blockсhainClient;

        private readonly ICustomerProvider _customerProvider;

        private readonly ILogger _logger;

        public PurchaseService(IShoppingCartService shoppingCartService, IBlockсhainClient blockсhainClient,
            ICustomerProvider customerProvider, ILoggerFactory loggerFactory)
        {
            _shoppingCartService = shoppingCartService;
            _blockсhainClient = blockсhainClient;
            _customerProvider = customerProvider;
            _logger = loggerFactory.CreateLogger<PurchaseService>();
        }

        public async Task<PurchaseResult> MakePurchase(Account account)
        {
            var result = await GetPurchaseResult(account);
            try
            {
                if (result.Status == PurchaseStatus.Success)
                {
                    await _shoppingCartService.SuccessPurchase(result);
                }
                else
                {
                    await _shoppingCartService.FailurePurchase(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при подтверждении оплаты");
            }

            return result;
        }

        public async Task<PurchaseResult> GetPurchaseResult(Account account)
        {
            try
            {
                var customerName = _customerProvider.GetCustomer(account.Id)?.ParticipantName ?? "Неизвестный участник";

                var wallet = await _blockсhainClient.GetWallet(account.Id);
                if (wallet == null)
                {
                    return PurchaseResult.Error("Кошелек участника не найден", customerName);
                }

                var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();
                if (shoppingCart.IsEmpty)
                {
                    return PurchaseResult.Error("Компьютерное зрение вернуло пустую корзину", customerName);
                }

                if (wallet.Balance < shoppingCart.Total)
                {
                    return PurchaseResult.Rejected(customerName, "Недостаточно баллов для покупки", shoppingCart,
                        wallet.Balance);
                }

                var transactionResponse = await _blockсhainClient.Pay(account.Id, shoppingCart.Total);

                const int max = 10;
                for (var i = 0; i < max; i++)
                {
                    var status = await _blockсhainClient.GetStatus(transactionResponse.TxHash);
                    if (status.IsSuccess)
                    {
                        return PurchaseResult.Success(customerName, shoppingCart, wallet.Balance - shoppingCart.Total,
                            transactionResponse.TxHash);
                    }

                    if (status.IsError)
                    {
                        return PurchaseResult.Rejected(customerName, "Транзакция отклонена blockchain", shoppingCart,
                            wallet.Balance, transactionResponse.TxHash);
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }

                _logger.LogWarning($"Статус транзакции неизвестен после {max} попыток");

                return PurchaseResult.Error("Статус транзакции неизвестен", customerName, transactionResponse.TxHash,
                    shoppingCart, wallet.Balance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при оплате");

                return PurchaseResult.UknownError(ex.ToString());
            }
        }
    }
}