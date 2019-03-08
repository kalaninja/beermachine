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

        public List<Buyer> TopBuyers { get; set; }

        public List<TransactionLogResponse> Log { get; set; }
    }

    public class TransactionLogResponse
    {
        public long Block { get; set; }
        
        public long Id { get; set; }

        public string TxHash { get; set; }

        public long Amount { get; set; }

        public short MessageId { get; set; }

        public long Seed { get; set; }

        public DateTime TransactionDate => new DateTime(Seed);
    }

    public class Transaction
    {
        public TransactionBody Body { get; set; }

        public string MessageId { get; set; }

        public string ProtocolVersion { get; set; }

        public string ServiceId { get; set; }

        public string Signature { get; set; }

        public bool IsIssue => MessageId == "0";
    }
}