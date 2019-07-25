using FlightAdvisor.API;
using FlightAdvisor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FlightAdvisor.Data
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.Cities.Any())
                {
                    return; 
                }

                context.Cities.AddRange(
                    new City
                    {
                        Id = 1,
                        Name = "Belgrade",
                        Country = "Serbia",
                        Description = "Capital of Serbia."
                    },
                    new City
                    {
                        Id = 2,
                        Name = "Rome",
                        Country = "Italy",
                        Description = "Capital of Italy."
                    });

                context.SaveChanges();
            }
        }
    }
}
