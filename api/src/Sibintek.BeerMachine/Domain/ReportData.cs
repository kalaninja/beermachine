using System;
using System.Collections.Generic;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Domain
{
    public class ReportData
    {
        public long CoinsTotal { get; set; }

        public long CoinsMined { get; set; }

        public long CoinsSpent { get; set; }

        public List<CustomerModel> TopRich { get; set; }

        public List<BuyerModel> TopBuyers { get; set; }

        public List<TransactionModel> Transactions { get; set; }
        
        public DateTime ReportDate { get; set; }
    }
}