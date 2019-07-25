using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightAdvisor.API.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
