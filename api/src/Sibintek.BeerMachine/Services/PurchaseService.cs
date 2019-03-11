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
            try
            {
                var customer = _customerProvider.GetCustomer(account.Id);

                var wallet = await _blockсhainClient.GetWallet(account.Id);
                if (wallet == null)
                {
                    return new PurchaseResult
                    {
                        Status = PurchaseStatus.Error,
                        ErrorDescription = "Кошелек участника не найден",
                        Customer = customer?.ParticipantName
                    };
                }

                var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();
                
                if (shoppingCart.IsEmpty)
                {
                    return new PurchaseResult
                    {
                        Status = PurchaseStatus.Error,
                        ErrorDescription = "Компьютерное зрение вернуло пустую корзину",
                        Customer = customer?.ParticipantName
                    };
                }

                if (wallet.Balance < shoppingCart.Total)
                {
                    return new PurchaseResult
                    {
                        ShoppingCart = shoppingCart,
                        Status = PurchaseStatus.Rejected,
                        ErrorDescription = "Недостаточно баллов для покупки",
                        WalletBalance = wallet.Balance,
                        Customer = customer?.ParticipantName
                    };
                }

                var transactionResponse = await _blockсhainClient.Pay(account.Id, shoppingCart.Total);

                const int max = 10;
                for (var i = 0; i < max; i++)
                {
                    var status = await _blockсhainClient.GetStatus(transactionResponse.TxHash);
                    if (status.IsSuccess)
                    {
                        return new PurchaseResult
                        {
                            Status = PurchaseStatus.Success,
                            ShoppingCart = shoppingCart,
                            WalletBalance = wallet.Balance - shoppingCart.Total,
                            TransactionHash = transactionResponse.TxHash,
                            Customer = customer?.ParticipantName
                        };
                    }

                    if (status.IsError)
                    {
                        return new PurchaseResult
                        {
                            Status = PurchaseStatus.Rejected,
                            Customer = customer?.ParticipantName,
                            ShoppingCart = shoppingCart,
                            TransactionHash = transactionResponse.TxHash,
                            WalletBalance = wallet.Balance,
                            ErrorDescription = "Транзакция отклонена blockchain"
                        };
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
                
                _logger.LogWarning($"Статус транзакции неизвестен после {max} попыток");
                
                return new PurchaseResult
                {
                    Status = PurchaseStatus.Error,
                    Customer = customer?.ParticipantName,
                    ShoppingCart = shoppingCart,
                    TransactionHash = transactionResponse.TxHash,
                    WalletBalance = wallet.Balance,
                    ErrorDescription = "Статус транзакции неизвестен"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при оплате");
                
                return new PurchaseResult
                {
                    Status = PurchaseStatus.Error,
                    ErrorDescription = "Неизвестная ошибка. Обратитесь к администратору системы",
                    ExceptionText = ex.ToString()
                };
            }
        }
    }
}