using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface IBlockсhainClient
    {
        Task Pay(long walletId, decimal sum);

        Task Issue(long walletId);

        Task<Wallet> GetWallet(long id);
    }
}