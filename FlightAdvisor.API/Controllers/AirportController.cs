using FlightAdvisor.Core.CustomExceptions;
using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/airports")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly ICheapestFlightService _cheapestFlightService;

        public AirportController(IAirportService airportService, ICheapestFlightService cheapestFlightService)
        {
            _airportService = airportService;
            _cheapestFlightService = cheapestFlightService;
        }

        [Route("find-cheapest-flight")]
        [HttpGet]
        public IActionResult GetCheapestFlight(string sourceCity, string destinationCity)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceCity) || string.IsNullOrEmpty(destinationCity))
                    return BadRequest("Source city and/or desination city fields are not valid.");

                var cheapestFlight = _cheapestFlightService.FindCheapestFlight(sourceCity, destinationCity);

                return Ok(cheapestFlight);
            }
            catch (NotFoundCityException)
            {
                return BadRequest("Source city and/or desination city airport does not exist.");
            }
            catch (CheapestRoutePriceIsInfinityException)
            {
                return BadRequest("Route between source city and destination city does not exist.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var airports = _airportService.GetAll();

            return Ok(airports);
        }

        [Route("import")]
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("Please attach the file.");

                var result = await _airportService.ImportFile(file);

                return Ok("Successfully imported rows: " + result.SuccessfullyImportedRows + ", skipped rows: " + result.SkippedRows);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}