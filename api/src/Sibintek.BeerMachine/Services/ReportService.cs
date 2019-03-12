using System;
using System.Linq;
using System.Threading.Tasks;
using Sibintek.BeerMachine.BlockchainClient;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public class ReportService : IReportService
    {
        private readonly ICustomerProvider _customerProvider;

        private readonly IBlockсhainClient _blockсhainClient;

        public ReportService(ICustomerProvider customerProvider, IBlockсhainClient blockсhainClient)
        {
            _customerProvider = customerProvider;
            _blockсhainClient = blockсhainClient;
        }

        public async Task<ReportData> GetReport()
        {
            var blockChainReport = await _blockсhainClient.Report();

            return new ReportData
            {
                CoinsMined = blockChainReport.CoinsMined,
                CoinsSpent = blockChainReport.CoinsSpent,
                CoinsTotal = blockChainReport.CoinsTotal,
                TopRich = blockChainReport.TopRich.Select(Map).OrderByDescending(x => x.Balance).ToList(),
                TopBuyers = blockChainReport.TopBuyers.Select(Map).OrderByDescending(x => x.Spent).ToList(),
                Transactions = blockChainReport.Log?.Select(Map).OrderByDescending(x => x.TransactionDate).ToList(),
                ReportDate = DateTime.Now,
                NodeStatuses = _blockсhainClient.NodeStatuses()
            };
        }

        private CustomerModel Map(Wallet wallet)
        {
            var customerName = _customerProvider.GetCustomer(wallet.Id)?.ParticipantName ?? "Неизвестный участник";

            return new CustomerModel
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                Name = customerName
            };
        }

        private BuyerModel Map(Buyer buyer)
        {
            var customerName = _customerProvider.GetCustomer(buyer.BuyerWallet.Id)?.ParticipantName ??
                               "Неизвестный участник";

            return new BuyerModel
            {
                Id = buyer.BuyerWallet.Id,
                Balance = buyer.BuyerWallet.Balance,
                Name = customerName,
                Spent = buyer.Spent
            };
        }

        private static TransactionModel Map(TransactionLogResponse transaction)
        {
            return new TransactionModel
            {
                Sum = transaction.Amount,
                Type = GetTransactionType(transaction.MessageId, transaction.Success),
                WalletId = transaction.Id,
                TransactionDate = transaction.TransactionDate,
                Hash = transaction.TxHash,
                Block = transaction.Block
            };
            
            string GetTransactionType(int messageId, bool success)
            {
                return success
                    ? (messageId == 0 ? "зачислено" : "списано")
                    : (messageId == 0 ? "не зачислено" : "не списано");
            }
        }
        
        
    }
}