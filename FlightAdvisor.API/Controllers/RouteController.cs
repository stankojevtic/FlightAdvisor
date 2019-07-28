using System;
using System.Threading.Tasks;
using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/routes")]
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
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("Please attach the file.");

                var result = await _routeService.ImportFile(file);

                return Ok("Successfully imported rows: " + result.SuccessfullyImportedRows + ", skipped rows: " + result.SkippedRows);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}