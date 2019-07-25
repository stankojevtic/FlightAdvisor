using System.ComponentModel.DataAnnotations;

namespace FlightAdvisor.API.DTO.City
{
    public class CityCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
