using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class BlockсhainClient : IBlockсhainClient
    {
        private readonly BlockchainOptions _options;

        public BlockсhainClient(BlockchainOptions options)
        {
            _options = options;
        }
        
        public Task Pay(long walletId, decimal sum)
        {
            return Task.CompletedTask;
        }

        public Task Issue(long walletId)
        {
            return Task.CompletedTask;
        }

        public Task<Wallet> GetWallet(long walletId)
        {
            return Task.FromResult(new Wallet(walletId, 100));
        }
        
        
    }
}