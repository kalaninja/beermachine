using System;
using System.Threading.Tasks;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IShoppingCartService _shoppingCartService;

        private readonly IBlockсhainClient _blockсhainClient;

        private readonly ICustomerProvider _customerProvider;
        
        public PurchaseService(IShoppingCartService shoppingCartService, IBlockсhainClient blockсhainClient, ICustomerProvider customerProvider)
        {
            _shoppingCartService = shoppingCartService;
            _blockсhainClient = blockсhainClient;
            _customerProvider = customerProvider;
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
                        Customer = customer?.Name
                    };
                }
                
                var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();
                if (shoppingCart.IsEmpty)
                {
                    return new PurchaseResult
                    {
                        Status = PurchaseStatus.Error,
                        ErrorDescription = "Компьютерное зрение вернуло пустую корзину",
                        Customer = customer?.Name
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
                        Customer = customer?.Name
                    };
                }               
                
                var transactionResponse = await _blockсhainClient.Pay(account.Id, shoppingCart.Total);
                return new PurchaseResult
                {
                    Status = PurchaseStatus.Success,
                    ShoppingCart = shoppingCart,
                    WalletBalance = wallet.Balance - shoppingCart.Total,
                    TransactionHash = transactionResponse.Hash,
                    Customer = customer?.Name
                };
            }
            catch (Exception ex)
            {
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