using System;

namespace Sibintek.BeerMachine.ErrorHandling
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}