using FluentValidation;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Bookings.CreateBooking.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.ResourceId)
                .GreaterThan(0).WithMessage("Resource ID must be greater than 0.");

            RuleFor(x => x.BookedQuantity)
                .GreaterThan(0).WithMessage("Booked Quantity must be greater than 0.");

            // 'DateFrom' ile 'DateTo' aynı gün olmasına izin ver
            RuleFor(x => x.DateFrom)
                .GreaterThan(DateTime.UtcNow.Date).WithMessage("Start time must be in the future.")
                .LessThan(x => x.DateTo).WithMessage("Start time must be earlier than end time.");

            RuleFor(x => x.DateTo)
                .GreaterThan(DateTime.UtcNow.Date).WithMessage("End time must be in the future.")
                .Must((x, dateTo) => dateTo.Date == x.DateFrom.Date || dateTo > x.DateFrom)
                .WithMessage("End time must be on the same day or after start time.");
        }
    }
}
