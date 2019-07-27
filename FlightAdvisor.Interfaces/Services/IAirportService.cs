using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightAdvisor.Interfaces.Services
{
    public interface IAirportService
    {
        Task<ImportInfoModel> ImportFile(IFormFile file);
        IEnumerable<Airport> GetAll();
    }
}
