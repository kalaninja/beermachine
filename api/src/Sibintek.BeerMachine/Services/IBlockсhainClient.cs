using System.Threading.Tasks;

namespace Sibintek.BeerMachine.Services
{
    public interface IBlockсhainClient
    {
        Task Transfer(string accountId, decimal sum);
    }
}