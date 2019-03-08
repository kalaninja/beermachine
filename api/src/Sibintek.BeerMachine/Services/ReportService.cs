using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
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
                TopBuyers = blockChainReport.TopBuyers.Select(Map).OrderByDescending(x => x.Balance).ToList(),
                Transactions = blockChainReport.Transactions?.Select(Map).OrderByDescending(x => x.TransactionDate).ToList(),
            };
        }

        private CustomerModel Map(Wallet wallet)
        {
            var customerName = _customerProvider.GetCustomer(wallet.Id)?.Fio ?? "Неизвестный участник";

            return new CustomerModel
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                Name = customerName
            };
        }

        private static TransactionModel Map(TransactionLogResponse transaction)
        {
            return new TransactionModel
            {
                Sum = transaction.Sum,
                Type = transaction.Type,
                WalletId = transaction.WalletId,
                TransactionDate = transaction.TransactionDate
            };
        }
    }
}