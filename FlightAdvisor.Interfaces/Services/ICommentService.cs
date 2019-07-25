using FlightAdvisor.Domain.Models;
using System;
using System.Collections.Generic;

namespace FlightAdvisor.Interfaces.Services
{
    public interface ICommentService
    {
        void Add(Comment comment);
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> GetAll(Func<Comment, bool> predicate);
        Comment Get(int id);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
