using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Rules
{
    public class SameDayBookingRule : IBookingConflictRule
    {
        public Task<bool> IsConflictAsync(IEnumerable<Booking> existingBookings, Booking newBooking, Resource resource)
        {
            if (newBooking.DateFrom.Date == newBooking.DateTo.Date)
            {
                if (existingBookings.Any(b => b.DateFrom.Date == newBooking.DateFrom.Date))
                {
                    throw new InvalidOperationException($"A booking already exists for {newBooking.DateFrom.Date.ToShortDateString()}. You cannot book the same day again.");
                }
            }
            return Task.FromResult(false);
        }
    }
}
