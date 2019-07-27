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
            var cheapestFlightModel = new CheapestFlightModel();
            var cheapestRouteForEachAirport = new List<CheapestFlightByAirport>();
            var startingNodes = _airportRepository.GetWhere(x => x.City == sourceCity);
            var destinationNodes = _airportRepository.GetWhere(x => x.City == destinationCity);

            foreach (var item in startingNodes)
            {
                var calculator = new DistanceCalculator();
                var distances = calculator.CalculateDistances(graph, item.IATA);

                var cheapestPerAirport = distances
                    .Where(x => destinationNodes.Any(y => x.Key == y.IATA))
                    .OrderBy(x => x.Value).FirstOrDefault();

                cheapestRouteForEachAirport.Add(new CheapestFlightByAirport
                {
                    Destination = cheapestPerAirport.Key,
                    Price = cheapestPerAirport.Value,
                    SourceAirport = item.IATA
                });
            }

            var cheapestRouteForAllAirports = cheapestRouteForEachAirport.OrderBy(x => x.Price).FirstOrDefault();

            return new CheapestFlightModel
            {
                Route = cheapestRouteForAllAirports.SourceAirport + " - " + cheapestRouteForAllAirports.Destination,
                TotalPrice = cheapestRouteForAllAirports.Price
            };
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
                graph.AddNode(item.IATA);
            }
        }
    }
}
