using System;
using System.Linq;
using System.Threading.Tasks;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Services
{
    public class WalletService : IWalletService
    {
        private  readonly ISessionService _sessionService;

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
                .Select(location => (
                        AccountId: location.Id,
                        Sum: program.FirstOrDefault(session => session.IsMatch(currentTime, location.Room))?.Points
                    )
                )
                .Where(pair => pair.Sum.HasValue)
                .Select(pair => (AccountId: pair.AccountId, Sum: pair.Sum.Value));    

            foreach (var pair in collectionToTransfer)
            {
                await _blockсhainClient.Transfer(pair.AccountId, pair.Sum);
            }
        }
    }
}