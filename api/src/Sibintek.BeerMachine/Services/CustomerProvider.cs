using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class CustomerProvider : ICustomerProvider
    {
        private ReadOnlyCollection<Customer> _customers;

        private readonly CustomerFileOptions _options;

        public CustomerProvider(CustomerFileOptions options)
        {
            _options = options;

            InitCustomerCollection();
        }

        private void InitCustomerCollection()
        {
            var text = File.ReadAllText(_options.FilePath);
            _customers = JsonConvert.DeserializeObject<List<Customer>>(text).AsReadOnly();
        }

        public Customer GetCustomer(long id)
        {
            var found = GetCustomerInternal(id);
            if (found != null)
            {
                return found;
            }

            InitCustomerCollection();
            return GetCustomerInternal(id);
        }

        private Customer GetCustomerInternal(long id)
            => _customers.FirstOrDefault(customer => customer.DevId == id);


        public void ClearCache()
            => InitCustomerCollection();
    }
}