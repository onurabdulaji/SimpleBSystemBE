using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Application.Rules;
using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.CreateBooking.Rules
{
    public class BookingConflictChecker : IBookingConflictChecker
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IEnumerable<IBookingConflictRule> _conflictRules;

        public BookingConflictChecker(IBookingRepository bookingRepository, IResourceRepository resourceRepository, IEnumerable<IBookingConflictRule> conflictRules)
        {
            _bookingRepository = bookingRepository;
            _resourceRepository = resourceRepository;
            _conflictRules = conflictRules;
        }

        public async Task<bool> IsBookingConflictAsync(int resourceId, DateTime startTime, DateTime endTime, int requestedQuantity)
        {
            var resource = await _resourceRepository.GetByIdAsync(resourceId);
            if (resource == null)
            {
                throw new InvalidOperationException("Resource not found.");
            }
            var existingBookings = await _bookingRepository.GetBookingsByResourceIdAsync(resourceId);

            var newBooking = new Booking
            {
                ResourceId = resourceId,
                DateFrom = startTime,
                DateTo = endTime,
                BookedQuantity = requestedQuantity
            };

            foreach (var rule in _conflictRules)
            {
                await rule.IsConflictAsync(existingBookings, newBooking, resource);
            }

            return true;
        }
    }
}
