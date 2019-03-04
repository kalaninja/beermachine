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
            var url = _options.NodeUrl + "/v1/wallet";
            
            return Task.FromResult(new Wallet(walletId, 100));
        }

        public Task<BlockchainReport> Report(int count = 10)
        {
            var url = _options.NodeUrl + "/v1/report";

            return Task.FromResult(new BlockchainReport());
        }
    }
}