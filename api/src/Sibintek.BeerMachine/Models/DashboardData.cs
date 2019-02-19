using System.Collections.Generic;

namespace Sibintek.BeerMachine.Models
{
    public class DashboardDataModel
    {
        public List<Customer> TopSavers { get; set; }
        
        public List<Customer> TopSpenders { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        
        public decimal Balance { get; set; }
    }
}