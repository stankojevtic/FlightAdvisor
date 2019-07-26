using FlightAdvisor.API;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Repositories;

namespace FlightAdvisor.Repositories.Repositories
{
    public class AirportRepository : BaseRepository<Airport>, IAirportRepository
    {
        public AirportRepository(DataContext context) : base(context)
        {
        }
    }
}
