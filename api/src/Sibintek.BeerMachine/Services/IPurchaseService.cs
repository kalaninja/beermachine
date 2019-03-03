using System.Threading.Tasks;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseResult> MakePurchase(Account account);
    }
}