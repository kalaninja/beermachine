namespace Sibintek.BeerMachine.Domain
{
    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public decimal Total => Price * Count;
    }
}