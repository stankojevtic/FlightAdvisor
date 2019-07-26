namespace FlightAdvisor.Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public int? AirlineId { get; set; }
        public string SourceAirport { get; set; }
        public int? SourceAirportId { get; set; }
        public string DestinationAirport { get; set; }
        public int? DestinationAirportId { get; set; }
        public string Codeshare { get; set; }
        public int Stops { get; set; }
        public string Equipment { get; set; }
        public double Price { get; set; }
    }
}
