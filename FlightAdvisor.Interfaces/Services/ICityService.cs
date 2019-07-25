using FlightAdvisor.Domain.Models;
using System;
using System.Collections.Generic;

namespace FlightAdvisor.Interfaces.Services
{
    public interface ICityService
    {
        void Add(City city);
        IEnumerable<City> GetAll();
        IEnumerable<City> GetAll(Func<City, bool> predicate);
    }
}
