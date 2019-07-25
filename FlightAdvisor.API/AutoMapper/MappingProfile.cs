using AutoMapper;
using FlightAdvisor.API.DTO;
using FlightAdvisor.API.DTO.City;
using FlightAdvisor.API.DTO.Comment;
using FlightAdvisor.Domain.Models;

namespace FlightAdvisor.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDTO>()
                .ReverseMap();

            CreateMap<City, CityCreateDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentCreateDTO>()
                .ReverseMap();

            CreateMap<Comment, CommentUpdateDTO>()
                .ReverseMap();
        }
    }
}
