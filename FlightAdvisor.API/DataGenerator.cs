using FlightAdvisor.API;
using FlightAdvisor.Core.Helpers;
using FlightAdvisor.Domain.Entities;
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
                if (context.Users.Any())
                {
                    return;
                }
            
                context.Users.AddRange(
                    new User
                    {                        
                        FirstName = "admin",
                        LastName = "admin",
                        Password = "30gTAU9XT0c3Oppc9xMXjCFrkv0=",
                        Role = Role.Admin,
                        Username = "admin",
                        Salt = "RFl4aA0JmbgJkkqGmJ5dY+GhWQXZ+yGzDKxPhm0Y9R0="
                    });

                context.SaveChanges();
            }
        }
    }
}
