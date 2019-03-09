using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class CustomerProvider : ICustomerProvider
    {
        private ReadOnlyCollection<Customer> _customers;

        private readonly CustomerFileOptions _options;

        private readonly ILogger _logger;

        public CustomerProvider(CustomerFileOptions options, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _options = options;

            _logger = loggerFactory.CreateLogger<CustomerProvider>();

            InitCustomerCollection(env.ContentRootPath);
        }

        private void InitCustomerCollection(string rootPath)
        {
            var path = Path.Combine(rootPath, _options.FilePath);
            var text = File.ReadAllText(path);
            _customers = JsonConvert.DeserializeObject<List<Customer>>(text).AsReadOnly();
        }

        public Customer GetCustomer(long id)
        {
            var found = GetCustomerInternal(id);
            if (found == null)
            {
                _logger.LogError($"Покупатель c Id {id} не найден в файле.");
            }

            return found;
        }

        private Customer GetCustomerInternal(long id) => _customers.FirstOrDefault(customer => customer.DevId == id);
    }
}