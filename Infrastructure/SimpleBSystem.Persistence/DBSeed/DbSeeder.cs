using SimpleBSystem.Domain.Entities;
using SimpleBSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.DBSeed
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Resources.Any())
            {
                context.Resources.AddRange(
                    new Resource
                    {
                        Name = "Resource 1",
                        Quantity = 500
                    },
                    new Resource
                    {
                        Name = "Resource 2",
                        Quantity = 500
                    },
                    new Resource
                    {
                        Name = "Resource 3",
                        Quantity = 500
                    }
                );
                context.SaveChanges();
            }

            if (!context.Bookings.Any())
            {
                context.Bookings.AddRange(
                    new Booking
                    {
                        ResourceId = 1,
                        BookedQuantity = 100,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }, new Booking
                    {
                        ResourceId = 2,
                        BookedQuantity = 100,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }, new Booking
                    {
                        ResourceId = 3,
                        BookedQuantity = 100,
                        DateFrom = DateTime.Now.AddDays(1),
                        DateTo = DateTime.Now.AddDays(2)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
