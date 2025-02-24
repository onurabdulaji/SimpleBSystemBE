using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Rules
{
    public class TimeOverlapRule : IBookingConflictRule
    {
        public Task<bool> IsConflictAsync(IEnumerable<Booking> existingBookings, Booking newBooking, Resource resource)
        {
            if (existingBookings.Any(b => b.DateFrom.Date < newBooking.DateTo.Date && b.DateTo.Date > newBooking.DateFrom.Date))
            {
                throw new InvalidOperationException("A booking already exists for the given time range. You cannot book during this period.");
            }
            return Task.FromResult(false);
        }
    }
}
