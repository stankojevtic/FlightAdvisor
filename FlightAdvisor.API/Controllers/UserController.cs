using System;
using System.Collections.Generic;
using System.Linq;
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
using FlightAdvisor.API.DTO.User;
using Microsoft.AspNetCore.Authorization;
using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Domain.Models;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("login")]
        public IActionResult Authenticate(string username, string password)
        {
            var user = _userService.Authenticate(username, password);

            if (user == null)
                return BadRequest("Username or password is incorrect.");

            var authenticatedUser = _mapper.Map<AuthenticationModel>(user);

            return Ok(authenticatedUser);
        }

        [Route("register")]
        [HttpPost]
        [ValidateModel]
        public IActionResult Register([FromBody] UserRegisterDTO userDto)
        {
            try
            {
                if (_userService.GetAll(x => x.Username == userDto.Username).Any())
                    return Conflict("User with username '" + userDto.Username + "' already exist.");
                
                var user = _mapper.Map<User>(userDto);
                _userService.Add(user);

                return Ok("User successfully added.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}