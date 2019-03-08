namespace Sibintek.BeerMachine.Models
{
    public class CustomerModel
    {
        public long Id { get; set; }

        public long Balance { get; set; }

        public string Name { get; set; }
    }

    public class BuyerModel: CustomerModel
    {
        public long Spent { get; set; }
    }
}