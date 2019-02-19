namespace Sibintek.BeerMachine.DataContracts
{
    public class Location : Account
    {
        public Room Room { get; set; }
    }

    public enum Room
    {
        Lobby = 1,
        CoffeeBreak,
        GrandHall,
        ConfHallA,
        ConfHallB,
        ConfHallC,
        Volga,
        Thames,
        Rhine,
        Don,
        Danube,
        Amur,
        Neva
    }
}