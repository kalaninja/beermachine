using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sibintek.BeerMachine.BlockchainClient;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Services;

public class MockBlockсhainClient : IBlockсhainClient
{
    public Task<TransactionStatus> GetStatus(string transactionHash)
    {
        return Task.FromResult<TransactionStatus>(new TransactionStatus {Type = "committed"});
    }

    public Task<TransactionResponse> Pay(long walletId, long sum)
    {
        return Task.FromResult(new TransactionResponse {TxHash = "ashdfkyasoi62934yp9gwfd87fsc"});
    }

    public Task<TransactionResponse> Issue(long walletId)
    {
        return Task.FromResult(new TransactionResponse() {TxHash = "ashdfkyasoi62934yp9gwfd87fsc"});
    }

    public Task<Wallet> GetWallet(long id)
    {
        return Task.FromResult(new Wallet
        {
            Id = new Random().Next(1, 11),
            Balance = new Random().Next(15, 101)
        });
    }

    public Task<BlockchainReportResponse> Report(int count = 10)
    {
        var random = new Random();

        return Task.FromResult(new BlockchainReportResponse()
        {
            TopRich = Enumerable.Range(1, 10).Select(x => new Wallet
            {
                Id = x,
                Balance = random.Next(15, 101)
            }).ToList(),

            TopBuyers = Enumerable.Range(1, 10).Select(x => new Buyer
            {
                BuyerWallet = new Wallet
                {
                    Id = x,
                    Balance = random.Next(15, 101)
                },
                Spent = random.Next(16, 200)
            }).ToList(),

            CoinsMined = random.Next(100, 2503),
            CoinsSpent = random.Next(272, 1273),
            CoinsTotal = random.Next(123, 1204),
            Log = Enumerable.Range(1, 11).Select(x => new TransactionLogResponse
            {
                Amount = random.Next(0, 200),
                Id = x,
                Block = random.Next(1000, 20002)
            }).ToList()
        });
    }

    public IDictionary<int, bool> NodeStatuses() => new Dictionary<int, bool> {{0, true}};
}