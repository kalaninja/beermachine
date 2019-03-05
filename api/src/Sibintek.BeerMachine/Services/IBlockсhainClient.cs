using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface IBlockсhainClient
    {
        Task<TransactionResponse> Pay(long walletId, decimal sum);

        Task<TransactionResponse> Issue(long walletId);

        Task<Wallet> GetWallet(long id);

        Task<BlockchainReport> Report(int count = 10);
    }
}