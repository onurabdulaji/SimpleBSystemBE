using MediatR;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.GetBooking.Query
{
    public class GetBookingsQuery : IRequest<List<GetBookingsResponseDto>>
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BookedQuantity { get; set; }
    }
}
