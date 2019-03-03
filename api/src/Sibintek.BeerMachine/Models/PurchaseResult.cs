using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Models
{
    public class PurchaseResult
    {
        public bool Success { get; set; }
        
        public string ErrorDescription { get; set; }
        
        public ShoppingCart ShoppingCart { get; set; }
        
        public decimal? WalletBalance { get; set; }
        
        public string ExceptionText { get; set; }
        
        public string Customer { get; set; }
    }
}