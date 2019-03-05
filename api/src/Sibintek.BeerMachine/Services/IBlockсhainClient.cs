using System.Collections.Generic;
using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public interface IBlockсhainClient
    {
        Task<TransactionResponse> Pay(long walletId, decimal sum);

        Task<TransactionResponse> Issue(long walletId);

        Task<Wallet> GetWallet(long id);

        Task<BlockchainReportResponse> Report(int count = 10);

        Task<List<TransactionInfo>> TransactionLog(int count = 10);
    }
}