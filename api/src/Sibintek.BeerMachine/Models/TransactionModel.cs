using System;

namespace Sibintek.BeerMachine.Models
{
    public class TransactionModel
    {
        public DateTime TransactionDate { get; set; }

        public string TransactionDateDisplayString => TransactionDate.ToString("dd.MM.yyyy HH:mm:ss");

        public long WalletId { get; set; }

        public int Type { get; set; }

        public decimal Sum { get; set; }
    }
}