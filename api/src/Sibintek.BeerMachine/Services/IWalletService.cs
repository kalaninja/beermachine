using System.Threading.Tasks;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Services
{
    public interface IWalletService
    {
        Task UpdateWallets(Location[] locations);
    }
}