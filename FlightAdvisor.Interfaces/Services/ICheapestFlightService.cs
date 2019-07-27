using FlightAdvisor.Domain.Models;

namespace FlightAdvisor.Interfaces.Services
{
    public interface ICheapestFlightService
    {
        CheapestFlightModel FindCheapestFlight(string sourceCity, string destinationCity);
    }
}
