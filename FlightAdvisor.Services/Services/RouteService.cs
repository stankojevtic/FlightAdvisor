using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Domain.Models;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Domain.Models;
using FlightAdvisor.Interfaces.Repositories;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightAdvisor.Core.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ImportInfoModel _importInfoModel;

        public RouteService(IRouteRepository routeRepository,
                            ICityRepository cityRepository,
                            IAirportRepository airportRepository)
        {
            _routeRepository = routeRepository;
            _airportRepository = airportRepository;
            _cityRepository = cityRepository;
            _importInfoModel = new ImportInfoModel();
        }

        public IEnumerable<Route> GetAll()
        {
            return _routeRepository.GetAll();
        }

        public async Task<ImportInfoModel> ImportFile(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var row = await reader.ReadLineAsync();
                    List<string> rowItems = row.Split(',').ToList();

                    AddRoute(rowItems);
                }
            }

            return _importInfoModel;
        }

        private void AddRoute(List<string> rowItems)
        {
            if(!AirportsExist(rowItems))
            {
                _importInfoModel.SkippedRows++;
                return;
            }

            var route = new Route
            {
                Airline = rowItems[0],
                AirlineId = ParserHelper.TryParseInt(rowItems[1]),
                SourceAirport = rowItems[2],
                SourceAirportId = ParserHelper.TryParseInt(rowItems[3]),
                DestinationAirport = rowItems[4],
                DestinationAirportId = ParserHelper.TryParseInt(rowItems[5]),
                Codeshare = rowItems[6],
                Stops = int.Parse(rowItems[7]),
                Equipment = rowItems[8],
                Price = double.Parse(rowItems[9])
            };

            _routeRepository.Add(route);
            _importInfoModel.SuccessfullyImportedRows++;
        }

        private bool AirportsExist(List<string> rowItems)
        {
            var routeSourceAirport = rowItems[2];
            var routeDestinationAirport = rowItems[4];

            var sourceAirportExist = _airportRepository
                .GetWhere(x => x.IATA == routeSourceAirport || x.ICAO == routeSourceAirport).Any();

            var destinationAirportExist = _airportRepository
                .GetWhere(x => x.IATA == routeDestinationAirport || x.ICAO == routeDestinationAirport).Any();

            if (sourceAirportExist && destinationAirportExist)
            {
                return true;
            }
            return false;
        }
    }
}
