using System.ComponentModel.DataAnnotations;

namespace FlightAdvisor.API.DTO.Comment
{
    public class CommentCreateDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int? CityId { get; set; }
    }
}
