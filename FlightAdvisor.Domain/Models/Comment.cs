using System;

namespace FlightAdvisor.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
