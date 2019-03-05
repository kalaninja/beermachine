using System.Collections.Generic;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public class BlockchainReportResponse
    {
        public long CoinsTotal { get; set; }
        
        public long CoinsMined { get; set; }
        
        public long CoinsSpent { get; set; }
        
        public List<Wallet> TopRich { get; set; }
        
        public List<Wallet> TopBuyers { get; set; }
    }
}