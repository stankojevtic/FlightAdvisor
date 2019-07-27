using FlightAdvisor.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FlightAdvisor.Interfaces.Services
{
    public interface IUserService
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        IEnumerable<User> GetAll(Func<User, bool> predicate);
        User Authenticate(string username, string password);
    }
}
