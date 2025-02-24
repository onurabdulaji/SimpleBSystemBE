using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.CreateBooking.Dtos
{
    public class CreateBookingResponseDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
