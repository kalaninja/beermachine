using System;
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
        
        public List<TransactionLogResponse> Transactions { get; set; }
    }

    public class TransactionLogResponse
    {
        public DateTime TransactionDate { get; set; }

        public long WalletId { get; set; }

        public int Type { get; set; }

        public decimal Sum { get; set; }
    }
}