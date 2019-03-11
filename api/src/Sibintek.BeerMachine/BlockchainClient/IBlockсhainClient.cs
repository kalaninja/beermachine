using System.Collections.Generic;
using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.BlockchainClient
{
    public interface IBlockсhainClient
    {
        Task<TransactionStatus> GetStatus(string transactionHash);

        Task<TransactionResponse> Pay(long walletId, long sum);

        Task<TransactionResponse> Issue(long walletId);

        Task<Wallet> GetWallet(long id);

        Task<BlockchainReportResponse> Report(int count = 10);

        IDictionary<int, bool> NodeStatuses();
    }
}