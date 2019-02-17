using System.Collections.Generic;

namespace Sibintek.BeerMachine.Domain
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }

        public decimal Total { get; set; }
    }
}