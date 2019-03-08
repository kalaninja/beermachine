using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Models
{
    public class PurchaseResult
    {
        public PurchaseStatus Status { get; set; }
        
        public ShoppingCart ShoppingCart { get; set; }
        
        public decimal? WalletBalance { get; set; }
        
        public string ExceptionText { get; set; }
        
        public string Customer { get; set; }
        
        public string ErrorDescription { get; set; }
        
        public string TransactionHash { get; set; }

        public static PurchaseResult Error(PurchaseStatus status, string errorDescription, string customerName, string exceptionText = null)
        {
            return new PurchaseResult
            {
                Status = status,
                ErrorDescription = errorDescription,
                Customer = customerName,
                ExceptionText = exceptionText
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