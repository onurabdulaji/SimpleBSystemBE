using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Rules
{
    public interface IBookingConflictRule
    {
        Task<bool> IsConflictAsync(IEnumerable<Booking> existingBookings, Booking newBooking, Resource resource);
    }
}
