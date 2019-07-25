using System;

namespace FlightAdvisor.Core.CustomExceptions
{
    public class NotFoundCityException : Exception
    {
        public NotFoundCityException()
        {
        }

        public NotFoundCityException(string message) : base(message)
        {
        }

        public NotFoundCityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
