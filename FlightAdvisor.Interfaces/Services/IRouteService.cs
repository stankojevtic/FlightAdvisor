using FlightAdvisor.Core.Models;
using FlightAdvisor.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightAdvisor.Interfaces.Services
{
    public interface IRouteService
    {
        Task<ImportInfoModel> ImportFile(IFormFile file);
        IEnumerable<Route> GetAll();
    }
}
