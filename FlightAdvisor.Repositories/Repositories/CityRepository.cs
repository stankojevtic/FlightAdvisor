using FlightAdvisor.API;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Repositories;

namespace FlightAdvisor.Repositories.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(DataContext context) : base(context)
        {
        }
    }
}
