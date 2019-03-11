using System;
using System.Linq;
using System.Threading.Tasks;
using Sibintek.BeerMachine.BlockchainClient;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Services
{
    public class WalletService : IWalletService
    {
        private readonly ISessionService _sessionService;

        private readonly IBlockсhainClient _blockсhainClient;

        public WalletService(ISessionService sessionService, IBlockсhainClient blockсhainClient)
        {
            _sessionService = sessionService;
            _blockсhainClient = blockсhainClient;
        }

        public async Task UpdateWallets(Location[] locations)
        {
            var currentTime = DateTime.Now;
            var program = _sessionService.GetProgram();

            var collectionToTransfer = locations
                .Where(location => program.Any(session => session.IsMatch(currentTime, location.Room)))
                .Select(location => location.Id);

            foreach (var accountId in collectionToTransfer)
            {
                await _blockсhainClient.Issue(accountId);
            }
        }
    }
}