using MediatR;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Dtos;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Query;
using SimpleBSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.GetBooking.Handlers
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, List<GetBookingsResponseDto>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<GetBookingsResponseDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var resources = await _bookingRepository.GetAllAsync();

            return resources.Select(r => new GetBookingsResponseDto
            {
                Id = r.Id,
                ResourceId = r.ResourceId,
                BookedQuantity = r.BookedQuantity,
                DateFrom = r.DateFrom,
                DateTo = r.DateTo
            }).ToList();
        }
    }
}
