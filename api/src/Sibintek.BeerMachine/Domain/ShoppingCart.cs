using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Linq;

namespace Sibintek.BeerMachine.Domain
{
    public class ShoppingCart
    {
        public ReadOnlyCollection<Item> Items { get; }

        public long Total { get; }

        public ShoppingCart(List<Item> items)
        {
            Items = items.AsReadOnly();
            Total = items.Sum(x => x.Total);
        }

        public bool IsEmpty => !Items.Any();

        public class Item
        {
            public string Name { get; }

            public decimal Price { get; }

            public int Count { get; }

            public long Total { get; }

            public Item(string name, long price, int count)
            {
                Name = name;
                Price = price;
                Count = count;
                Total = price * count;
            }
        }
    }
}