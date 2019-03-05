using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            //var fileText = File.ReadAllText(_options.FilePath);

            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Иванов Иван"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Петров Семен"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Смирнов Петр"
                },
            }.AsReadOnly();
        }

        public Customer GetCustomer(long id)
        {
            return _customers.FirstOrDefault(customer => customer.Id == id);
        }

        public void ClearCache()
        {
            InitCustomerCollection();
        }
    }
}