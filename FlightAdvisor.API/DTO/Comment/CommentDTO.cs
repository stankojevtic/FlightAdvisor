using System;

namespace FlightAdvisor.API.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }        
        public string Description { get; set; }
        public int CityId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
