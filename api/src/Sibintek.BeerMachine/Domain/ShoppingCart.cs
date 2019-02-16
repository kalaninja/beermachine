using System.Collections.Generic;

namespace Sibintek.BeerMachine.Domain
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public decimal Total { get; set; }
    }
}