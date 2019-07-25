using FlightAdvisor.Domain.Models;
using FlightAdvisor.Interfaces.Repositories;
using FlightAdvisor.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace FlightAdvisor.Core.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public void Add(City city)
        {
            _cityRepository.Add(city);
        }

        public IEnumerable<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public IEnumerable<City> GetAll(Func<City, bool> predicate)
        {
            return _cityRepository.GetWhere(predicate);
        }
    }
}
