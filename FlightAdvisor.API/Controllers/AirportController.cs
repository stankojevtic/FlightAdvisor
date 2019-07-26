using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var airports = _airportService.GetAll();

            return Ok(airports);
        }

        [Route("import")]
        [HttpPost]
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