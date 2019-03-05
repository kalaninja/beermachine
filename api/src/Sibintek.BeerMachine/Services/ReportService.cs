using System;
using System.Collections.Generic;
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
            var blockChainReport =  await _blockсhainClient.Report();

            var transactions = await _blockсhainClient.TransactionLog();

            return new ReportData
            {
                CoinsMined = blockChainReport.CoinsMined,
                CoinsSpent = blockChainReport.CoinsSpent,
                CoinsTotal = blockChainReport.CoinsTotal,
                TopRich = blockChainReport.TopRich.Select(Map).OrderByDescending(x=>x.Balance).ToList(),
                TopBuyers = blockChainReport.TopBuyers.Select(Map).OrderByDescending(x=>x.Balance).ToList(),
                Transactions = transactions
            };
        }

        private CustomerInfo Map(Wallet wallet)
        {
            var customerName = _customerProvider.GetCustomer(wallet.Id)?.Name ?? "Неизвестный участник";

            return new CustomerInfo
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                Name = customerName
            };
        }
    }

    public class ReportData
    {
        public long CoinsTotal { get; set; }
        
        public long CoinsMined { get; set; }
        
        public long CoinsSpent { get; set; }
        
        public List<CustomerInfo> TopRich { get; set; }
        
        public List<CustomerInfo> TopBuyers { get; set; }
        
        public List<TransactionInfo> Transactions { get; set; }
    }

    public class CustomerInfo
    {
        public long Id { get; set; }
        
        public long Balance { get; set; }
        
        public string Name { get; set; }
    }
    
    
    public enum TransactionType
    {
        Addition = 0,
        Subtraction
    }

    public class TransactionInfo
    {
        public DateTime TransactionDate { get; set; }

        public string TransactionDateDisplayString => TransactionDate.ToString("dd.MM.yyyy HH:mm:ss");
        
        public long WalletId { get; set; }
        
        public TransactionType Type { get; set; }
        
        public decimal Sum { get; set; }
    }
}