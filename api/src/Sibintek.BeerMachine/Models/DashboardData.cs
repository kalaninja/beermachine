using System;
using System.Collections.Generic;

namespace Sibintek.BeerMachine.Models
{
    public class DashboardDataModel
    {
        public List<CustomerModel> TopSavers { get; set; }
        
        public List<CustomerModel> TopSpenders { get; set; }
        
        public int[] EarnedLostDataSet { get; set; }
        
        public int[] SpendAccumulateDataSet { get; set; }
        
        public List<TransactionModel> Transactions { get; set; }

        public string CurrentDate => DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
    }

    public class CustomerModel
    {
        public string Name { get; set; }
        
        public decimal Balance { get; set; }
    }

    public enum TransactionType
    {
        Addition = 0,
        Subtraction
    }

    public class TransactionModel
    {
        public DateTime TransactionDate { get; set; }

        public string TransactionDateDisplayString => TransactionDate.ToString("dd.MM.yyyy HH:mm:ss");
        
        public long WalletId { get; set; }
        
        public TransactionType Type { get; set; }
        
        public decimal Sum { get; set; }
    }
}