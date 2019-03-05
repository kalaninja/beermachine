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
        
    }

    public enum PurchaseStatus
    {
        Success = 0,
        Rejected,
        Error
    }
}