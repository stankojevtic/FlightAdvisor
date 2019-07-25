using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
