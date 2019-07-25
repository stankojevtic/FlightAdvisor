using FlightAdvisor.API;
using FlightAdvisor.Domain.Models;
using FlightAdvisor.Interfaces.Repositories;

namespace FlightAdvisor.Repositories.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
    }
}
