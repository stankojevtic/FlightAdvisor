using System.Collections.Generic;

namespace FlightAdvisor.Domain.Models
{
    public class GraphResultModel
    {
        public string Key { get; set; }
        public double Distance {get;set;}
        public List<string> RouteNames { get; set; }
        public List<double> RoutePrices { get; set; }
    }
}
