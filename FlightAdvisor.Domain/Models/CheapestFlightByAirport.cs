namespace FlightAdvisor.Domain.Models
{
    public class CheapestFlightByAirport
    {
        public string Destination { get; set; }
        public double Price { get; set; }
        public string SourceAirport { get; set; }
    }
}
