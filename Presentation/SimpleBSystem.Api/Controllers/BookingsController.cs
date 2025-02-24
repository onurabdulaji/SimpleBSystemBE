using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Commands;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Query;

namespace SimpleBSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var result = await _mediator.Send(new GetBookingsQuery());
            return Ok(result);
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
