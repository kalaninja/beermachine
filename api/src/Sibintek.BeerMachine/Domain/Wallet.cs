using Newtonsoft.Json;

namespace Sibintek.BeerMachine.Domain
{
    public class Wallet
    {
        public long Balance { get;  set; }
        
        public long Id { get; set; }
    }

    public class Buyer
    {
        [JsonProperty("buyer")]
        public Wallet BuyerWallet { get; set; }
        
        public long Spent { get; set; }
    }
}