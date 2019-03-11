using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Models
{
    public class PurchaseResult
    {
        public PurchaseStatus Status { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public long? WalletBalance { get; set; }

        public string ExceptionText { get; set; }

        public string Customer { get; set; }

        public string ErrorDescription { get; set; }

        public string TransactionHash { get; set; }
        
        public static PurchaseResult UknownError(string exceptionText)
        {
            return new PurchaseResult
            {
                Status = PurchaseStatus.Error,
                ErrorDescription = "Неизвестная ошибка. Обратитесь к администратору системы",
                ExceptionText = exceptionText
            };
        }

        public static PurchaseResult Error(string errorDescription, string customerName, string transactionHash = null,ShoppingCart shoppingCart = null, long? walletBalance = null)
        {
            return new PurchaseResult
            {
                Status = PurchaseStatus.Error,
                ErrorDescription = errorDescription,
                Customer = customerName,
                TransactionHash = transactionHash,
                ShoppingCart = shoppingCart,
                WalletBalance = walletBalance
            };
        }

        public static PurchaseResult Rejected(string customerName, string errorDescription, ShoppingCart shoppingCart,
            long walletBalance, string transactionHash = null)
        {
            return new PurchaseResult
            {
                Status = PurchaseStatus.Rejected,
                Customer = customerName,
                ErrorDescription = errorDescription,
                ShoppingCart = shoppingCart,
                WalletBalance = walletBalance,
                TransactionHash = transactionHash
            };
        }

        public static PurchaseResult Success(string customerName, ShoppingCart shoppingCart, long walletBalance, string transactionHash)
        {
            return new PurchaseResult
            {
                Status = PurchaseStatus.Success,
                ShoppingCart = shoppingCart,
                Customer = customerName,
                WalletBalance = walletBalance,
                TransactionHash = transactionHash
            };
        }
    }

    public enum PurchaseStatus
    {
        Success = 0,
        Rejected,
        Error
    }
}