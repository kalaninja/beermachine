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
        
        public PurchaseService(IShoppingCartService shoppingCartService, IBlockсhainClient blockсhainClient)
        {
            _shoppingCartService = shoppingCartService;
            _blockсhainClient = blockсhainClient;
        }
        
        public async Task<PurchaseResult> MakePurchase(Account account)
        {
            try
            {
                var wallet = await _blockсhainClient.GetWallet(account.Id);
                if (wallet == null)
                {
                    return new PurchaseResult
                    {
                        Success = false,
                        ErrorDescription = "Кошелек участника не найден",
                    };
                }
                
                var shoppingCart = await _shoppingCartService.GetCurrentShoppingCart();
                if (shoppingCart.IsEmpty)
                {
                    return new PurchaseResult
                    {
                        Success = false,
                        ErrorDescription = "Компьютерное зрение вернуло пустую корзину"
                    };
                }
                
                
                if (wallet.Balance < shoppingCart.Total)
                {
                    return new PurchaseResult
                    {
                        Success = false,
                        ErrorDescription = "Недостаточно средств для покупки",
                        WalletBalance = wallet.Balance
                    };
                }
                else
                {
                    await _blockсhainClient.Pay(account.Id, shoppingCart.Total);

                    return new PurchaseResult
                    {
                        Success = true,
                        ShoppingCart = shoppingCart,
                        WalletBalance = wallet.Balance - shoppingCart.Total
                    };
                }
            }
            catch (Exception ex)
            {
                return new PurchaseResult
                {
                    Success = false,
                    ErrorDescription = "Неизвестная ошибка",
                    ExceptionText = ex.ToString()
                };
            }
        }
    }
}