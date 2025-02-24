using FluentValidation;
using MediatR;
using SimpleBSystem.Application.CommonHelpers.Interface;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Commands;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Dtos;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Rules;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Domain.Entities;

namespace SimpleBSystem.Application.Features.Bookings.CreateBooking.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingConflictChecker _bookingConflictChecker;
        private readonly IEmailSender _emailRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IBookingConflictChecker bookingConflictChecker, IEmailSender emailRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingConflictChecker = bookingConflictChecker;
            _emailRepository = emailRepository;
        }

        public async Task<CreateBookingResponseDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            bool isConflict = await _bookingConflictChecker.IsBookingConflictAsync
                (
                request.ResourceId,
                request.DateFrom,
                request.DateTo,
                request.BookedQuantity
                );
            if (!isConflict)
            {
                throw new ValidationException("Booking conflict detected! Please choose a different date or quantity.");
            }

            var booking = new Booking
            {
                ResourceId = request.ResourceId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                BookedQuantity = request.BookedQuantity
            };

            await _bookingRepository.AddAsync(booking);

            await _emailRepository.SendBookingConfirmationConsoleEmailAsync(booking.Id);

            return new CreateBookingResponseDto
            {
                Id = booking.Id,
                Message = "Booking successfully created!"
            };
        }
    }
}
