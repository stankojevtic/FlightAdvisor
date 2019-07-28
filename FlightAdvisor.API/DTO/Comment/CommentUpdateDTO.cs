using System.ComponentModel.DataAnnotations;

namespace FlightAdvisor.API.DTO.Comment
{
    public class CommentUpdateDTO
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int? CityId { get; set; }
    }
}
