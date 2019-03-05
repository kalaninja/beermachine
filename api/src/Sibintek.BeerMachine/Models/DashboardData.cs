using System;
using System.Collections.Generic;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.Models
{
    public class DashboardDataModel
    {
        public List<CustomerModel> TopSavers { get; set; }
        
        public List<CustomerModel> TopSpenders { get; set; }
        
        public int[] EarnedLostDataSet { get; set; }
        
        public int[] SpendAccumulateDataSet { get; set; }
        
        public List<TransactionInfo> Transactions { get; set; }

        public string CurrentDate => DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
    }

    public class CustomerModel
    {
        public string Name { get; set; }
        
        public decimal Balance { get; set; }
    }
}