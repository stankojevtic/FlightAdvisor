using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/airport")]
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

        [Route("cheapest-flight")]
        [HttpGet]
        public IActionResult GetCheapestFlight(string sourceCity, string destinationCity)
        {
            var airports = _cheapestFlightService.FindCheapestFlight(sourceCity, destinationCity);

            return Ok(airports);
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
                var result = await _airportService.ImportFile(file);

                return Ok("Successfully imported rows: " + result.SuccessfullyImportedRows + ", skipped rows: " + result.SkippedRows);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}