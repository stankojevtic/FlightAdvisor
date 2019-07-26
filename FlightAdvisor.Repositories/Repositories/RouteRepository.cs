using FlightAdvisor.API;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Repositories;

namespace FlightAdvisor.Repositories.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(DataContext context) : base(context)
        {
        }
    }
}
