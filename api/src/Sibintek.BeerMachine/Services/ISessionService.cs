using System.Collections.Generic;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface ISessionService
    {
        IReadOnlyList<Session> GetProgram();
    }
}