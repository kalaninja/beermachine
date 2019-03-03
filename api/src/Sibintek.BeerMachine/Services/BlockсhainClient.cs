using System.Threading.Tasks;

namespace Sibintek.BeerMachine.Services
{
    public class BlockсhainClient : IBlockсhainClient
    {
        public Task Transfer(string accountId, decimal sum)
        {
            return Task.CompletedTask;
        }
    }
}