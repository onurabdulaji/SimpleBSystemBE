using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.CreateBooking.Rules
{
    public interface IBookingConflictChecker
    {
        Task<bool> IsBookingConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int requestedQuantity);

    }
}
