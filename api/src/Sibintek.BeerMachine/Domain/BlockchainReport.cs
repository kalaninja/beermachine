using System.Collections.Generic;

namespace Sibintek.BeerMachine.Domain
{
    public class BlockchainReport
    {
        public long CoinsTotal { get; set; }
        
        public long CoinsMined { get; set; }
        
        public long CoinsSpent { get; set; }
        
        public List<Wallet> TopRich { get; set; }
        
        public List<Wallet> TopBuyers { get; set; }
    }
}