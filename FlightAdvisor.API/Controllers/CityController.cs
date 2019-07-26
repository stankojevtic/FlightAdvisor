using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightAdvisor.API.DTO;
using FlightAdvisor.API.DTO.City;
using FlightAdvisor.API.Validation;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, ICommentService commentService, IMapper mapper)
        {
            _cityService = cityService;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(string searchText = null, int? numberOfComments = null)
        {
            var cities = _cityService.GetAll(x => searchText == null || x.Name.Contains(searchText));
            var citiesDto = _mapper.Map<IEnumerable<CityDTO>>(cities);

            //bad way, but not sure how to do it using in-memory database, in real DB this foreach is not needed or if LazyLoading is enabled then we can use .Include
            foreach (var city in citiesDto)
            {
                var comments = _mapper
                    .Map<IEnumerable<CommentDTO>>(_commentService.GetAll(x => x.CityId == city.Id));
                city.Comments = comments
                    .TakeLast(numberOfComments == null ? comments.Count() : (int)numberOfComments).ToList();
            }

            return Ok(citiesDto);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Create([FromBody] CityCreateDTO cityDto)
        {
            try
            {
                var city = _mapper.Map<City>(cityDto);
                _cityService.Add(city);

                return Ok("City successfully added.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
