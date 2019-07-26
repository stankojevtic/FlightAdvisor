using AutoMapper;
using FlightAdvisor.API.DTO;
using FlightAdvisor.API.DTO.Comment;
using FlightAdvisor.API.Validation;
using FlightAdvisor.Core.CustomExceptions;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FlightAdvisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _commentService.GetAll();
            var commentsDto = _mapper.Map<IEnumerable<CommentDTO>>(comments);

            return Ok(commentsDto);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Create([FromBody] CommentCreateDTO commentDto)
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDto);
                _commentService.Add(comment);

                return Ok("Comment successfully added.");
            }
            catch (NotFoundCityException)
            {
                return NotFound("City with id " + commentDto.CityId + " does not exist.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [ValidateModel]
        public IActionResult Update([FromBody] CommentUpdateDTO commentDto)
        {
            try
            {
                var comment = _commentService.Get((int)commentDto.Id);
                if (comment == null)
                    return NotFound("Comment with id " + commentDto.Id + " does not exist.");

                comment = _mapper.Map(commentDto, comment);
                _commentService.Update(comment);

                return Ok("Comment successfully updated.");
            }
            catch (NotFoundCityException)
            {
                return NotFound("City with id " + commentDto.CityId + " does not exist.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var comment = _commentService.Get(id);
                if (comment == null)
                    return NotFound("City with id " + id + " does not exist.");

                _commentService.Delete(comment);

                return Ok("Comment successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}