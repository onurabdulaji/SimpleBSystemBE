using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Rules
{
    public class ResourceCapacityRule : IBookingConflictRule
    {
        public Task<bool> IsConflictAsync(IEnumerable<Booking> existingBookings, Booking newBooking, Resource resource)
        {
            var totalQuantityBooked = existingBookings.Sum(b => b.BookedQuantity);
            if (totalQuantityBooked + newBooking.BookedQuantity > resource.Quantity)
            {
                throw new InvalidOperationException($"The requested quantity exceeds available capacity. Max available: {resource.Quantity - totalQuantityBooked}.");
            }
            return Task.FromResult(false);
        }
    }
}
