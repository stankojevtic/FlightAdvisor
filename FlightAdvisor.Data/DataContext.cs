using FlightAdvisor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FlightAdvisor.API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            if (modifiedEntries != null && modifiedEntries.Any())
            {
                var currentTime = DateTime.Now;

                foreach (var entry in modifiedEntries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Entity.GetType().GetProperty("CreatedDate") != null)
                        {
                            entry.Property("ModifiedDate").CurrentValue = null;
                            entry.Property("CreatedDate").CurrentValue = currentTime;
                        }
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (entry.Entity.GetType().GetProperty("ModifiedDate") != null)
                        {
                            entry.Property("ModifiedDate").CurrentValue = currentTime;
                            entry.Property("CreatedDate").IsModified = false;
                        }
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
