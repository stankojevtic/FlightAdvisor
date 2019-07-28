using FlightAdvisor.Core.CustomExceptions;
using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Domain.Models;
using FlightAdvisor.Interfaces.Repositories;
using FlightAdvisor.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace FlightAdvisor.Core.Services
{
    public class CheapestFlightService : ICheapestFlightService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRouteRepository _routeRepository;

        public CheapestFlightService(
            IAirportRepository airportRepository,
            ICityRepository cityRepository,
            IRouteRepository routeRepository)
        {
            _airportRepository = airportRepository;
            _cityRepository = cityRepository;
            _routeRepository = routeRepository;
        }

        public CheapestFlightModel FindCheapestFlight(string sourceCity, string destinationCity)
        {
            Graph graph = new Graph();

            AddNodes(graph);
            AddConnections(graph);

            return FindCheapestRoute(sourceCity, destinationCity, graph);
        }

        private CheapestFlightModel FindCheapestRoute(string sourceCity, string destinationCity, Graph graph)
        {
            var cheapestRouteForEachAirport = new List<CheapestFlightModel>();
            var startingNodes = _airportRepository.GetWhere(x => x.City == sourceCity);
            var destinationNodes = _airportRepository.GetWhere(x => x.City == destinationCity);

            if (startingNodes.Count() == 0 || destinationNodes.Count() == 0)
                throw new NotFoundCityException();

            foreach (var item in startingNodes)
            {
                var calculator = new DistanceCalculator();

                var graphResults = calculator.CalculateDistances(graph, item.IATA != null ? item.IATA : item.ICAO);

                var cheapestPerAirport = graphResults
                    .Where(x => destinationNodes.Any(y => x.Key == y.IATA))
                    .OrderBy(x => x.Distance).FirstOrDefault();

                cheapestPerAirport.RouteNames.Add(cheapestPerAirport.Key);
                cheapestRouteForEachAirport.Add(new CheapestFlightModel
                {
                    Route = string.Join(" -> ", GetAirportCityNames(cheapestPerAirport.RouteNames.Distinct().ToList())),
                    TotalPrice = cheapestPerAirport.Distance
                });
            }

            return FindCheapestRouteOfAllAirports(cheapestRouteForEachAirport);
        }

        private List<string> GetAirportCityNames(List<string> routeNames)
        {
            var airportCityNames = new List<string>();
            foreach(var item in routeNames)
            {
                airportCityNames.Add(_airportRepository.GetWhere(x => x.IATA == item).FirstOrDefault().City);
            }
            return airportCityNames;
        }

        private void AddConnections(Graph graph)
        {
            var routes = _routeRepository.GetAll();
            foreach (var item in routes)
            {
                graph.AddConnection(item.SourceAirport, item.DestinationAirport, item.Price);
            }
        }

        private void AddNodes(Graph graph)
        {
            var airports = _airportRepository.GetAll();
            foreach (var item in airports)
            {
                    graph.AddNode(item.IATA != null ? item.IATA : item.ICAO);
            }
        }

        private CheapestFlightModel FindCheapestRouteOfAllAirports(List<CheapestFlightModel> cheapestRouteForEachAirport)
        {
            var cheapestRouteForAllAirports = cheapestRouteForEachAirport.OrderBy(x => x.TotalPrice).FirstOrDefault();

            if (double.IsInfinity(cheapestRouteForAllAirports.TotalPrice))
                throw new CheapestRoutePriceIsInfinityException();

            return new CheapestFlightModel
            {
                Route = cheapestRouteForAllAirports.Route,
                TotalPrice = cheapestRouteForAllAirports.TotalPrice
            };
        }
    }
}
