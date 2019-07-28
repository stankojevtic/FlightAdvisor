using System;

namespace FlightAdvisor.Core.CustomExceptions
{
    public class CheapestRoutePriceIsInfinityException : Exception
    {
        public CheapestRoutePriceIsInfinityException()
        {
        }

        public CheapestRoutePriceIsInfinityException(string message) : base(message)
        {
        }

        public CheapestRoutePriceIsInfinityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

