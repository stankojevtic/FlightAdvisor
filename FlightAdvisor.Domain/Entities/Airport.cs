namespace FlightAdvisor.Domain.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude{ get; set; }
        public int Altitude { get; set; }
        public double? Timezone { get; set; }
        public string DST { get; set; }
        public string TzDatabaseTimezone { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
