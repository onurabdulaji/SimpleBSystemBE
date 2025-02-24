using Microsoft.EntityFrameworkCore;
using SimpleBSystem.Domain.Entities;
using SimpleBSystem.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BookingConfiguration());
            builder.ApplyConfiguration(new ResourceConfiguration());
        }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
