using System;
using System.Threading.Tasks;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var routes = _routeService.GetAll();

            return Ok(routes);
        }

        [Route("import")]
        [HttpPost]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            try
            {
                var result = await _routeService.ImportFile(file);

                return Ok("Successfully imported rows: " + result.SuccessfullyImportedRows + ", skipped rows: " + result.SkippedRows);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}