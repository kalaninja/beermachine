using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Sibintek.BeerMachine.DataContracts;

namespace Sibintek.BeerMachine.Domain
{
    public class Session
    {
        public DateTime Start { get; }
        
        public DateTime End { get; }
        
        public Room[] Rooms { get; }
        
        public string Name { get; }

        public decimal Points { get; } = 0;
        
        public Session(string name, DateTime start, DateTime end, Room[] rooms)
        {
            Name = name;
            Start = start;
            End = end;
            Rooms = rooms;
        }

        public bool IsMatch(DateTime time, Room room)
            => time >= Start && time < End && Rooms.Contains(room);
    }
}