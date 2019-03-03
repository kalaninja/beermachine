using System;

namespace Sibintek.BeerMachine.Domain
{
    public class Wallet
    {
        public Wallet(long id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public decimal Balance { get; }
        
        public long Id { get; }
    }
}