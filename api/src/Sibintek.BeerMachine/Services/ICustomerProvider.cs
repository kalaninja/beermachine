using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface ICustomerProvider
    {
        Customer GetCustomer(long id);
        
        void ClearCache();
    }
}