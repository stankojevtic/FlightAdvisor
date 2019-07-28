using FlightAdvisor.Core.Helpers;
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
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ImportInfoModel _importInfoModel;

        public AirportService(IAirportRepository airportRepository, ICityRepository cityRepository)
        {
            _airportRepository = airportRepository;
            _cityRepository = cityRepository;
            _importInfoModel = new ImportInfoModel();
        }

        public IEnumerable<Airport> GetAll()
        {
            return _airportRepository.GetAll();
        }

        public async Task<ImportInfoModel> ImportFile(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var row = await reader.ReadLineAsync();
                    List<string> rowItems = row.Split(',').ToList();

                    AddAirport(rowItems);
                }
            }

            return _importInfoModel;
        }

        private void AddAirport(List<string> rowItems)
        {
            if (!_cityRepository.GetWhere(x => x.Name == rowItems[2].Trim('"')).Any())
            {
                _importInfoModel.SkippedRows++;
                return;
            }

            if (_airportRepository.Get(x => x.Id == int.Parse(rowItems[0])) == null)
            {
                var airport = new Airport
                {
                    Id = int.Parse(rowItems[0]),
                    Name = rowItems[1].Trim('"'),
                    City = rowItems[2].Trim('"'),
                    Country = rowItems[3].Trim('"'),
                    IATA = string.IsNullOrEmpty(rowItems[4]) || rowItems[4] == "\\N" ? null : rowItems[4].Trim('"'),
                    ICAO = string.IsNullOrEmpty(rowItems[5]) || rowItems[5] == "\\N" ? null : rowItems[5].Trim('"'),
                    Latitude = decimal.Parse(rowItems[6]),
                    Longitude = decimal.Parse(rowItems[7]),
                    Altitude = int.Parse(rowItems[8]),
                    Timezone = ParserHelper.TryParseDouble(rowItems[9]),
                    DST = rowItems[10].Trim('"'),
                    TzDatabaseTimezone = rowItems[11].Trim('"'),
                    Type = rowItems[12].Trim('"'),
                    Source = rowItems[13].Trim('"'),
                };
                _airportRepository.Add(airport);
                _importInfoModel.SuccessfullyImportedRows++;
            }
            else
            {
                _importInfoModel.SkippedRows++;
            }
        }    
    }
}
