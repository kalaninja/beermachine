namespace Sibintek.BeerMachine.DataContracts
{
    public class Location : Account
    {
        public Room Room { get; set; }
    }

    public enum Room
    {
        Lobby = 1,
        CoffeeBreak = 2,
        GrandHall = 3,
        ConfHallA = 4,
        ConfHallB = 5,
        ConfHallC = 6,
        Volga = 7,
        Thames = 8,
        Rhine = 9,
        Don = 10,
        Danube = 11,
        Amur = 12,
        Neva = 13
    }
}