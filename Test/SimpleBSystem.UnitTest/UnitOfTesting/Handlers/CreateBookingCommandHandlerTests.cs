using FluentAssertions;
using FluentValidation;
using Moq;
using SimpleBSystem.Application.CommonHelpers.Interface;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Commands;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Handlers;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Rules;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Domain.Entities;

namespace SimpleBSystem.UnitTest.UnitOfTesting.Handlers
{
    public class CreateBookingCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly Mock<IBookingConflictChecker> _mockBookingConflictChecker;
        private readonly Mock<IEmailSender> _mockEmailSender;
        private readonly CreateBookingCommandHandler _handler;

        public CreateBookingCommandHandlerTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockBookingConflictChecker = new Mock<IBookingConflictChecker>();
            _mockEmailSender = new Mock<IEmailSender>();
            _handler = new CreateBookingCommandHandler(_mockBookingRepository.Object, _mockBookingConflictChecker.Object, _mockEmailSender.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_CreatesBookingAndSendsEmail()
        {
            var command = new CreateBookingCommand
            {
                ResourceId = 1,
                BookedQuantity = 5,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddHours(2)
            };

            var bookingConflictCheckerResult = true;
            _mockBookingConflictChecker.Setup(r => r.IsBookingConflictAsync(command.ResourceId, command.DateFrom, command.DateTo, command.BookedQuantity))
                .ReturnsAsync(bookingConflictCheckerResult);

            _mockBookingRepository.Setup(r => r.AddAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);

            _mockEmailSender.Setup(e => e.SendBookingConfirmationConsoleEmailAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Message.Should().Be("Booking successfully created!");
            _mockBookingRepository.Verify(r => r.AddAsync(It.IsAny<Booking>()), Times.Once);
            _mockEmailSender.Verify(e => e.SendBookingConfirmationConsoleEmailAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_BookingConflict_ThrowsValidationException()
        {
            var command = new CreateBookingCommand
            {
                ResourceId = 1,
                BookedQuantity = 5,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddHours(2)
            };

            var bookingConflictCheckerResult = false;
            _mockBookingConflictChecker.Setup(r => r.IsBookingConflictAsync(command.ResourceId, command.DateFrom, command.DateTo, command.BookedQuantity))
                .ReturnsAsync(bookingConflictCheckerResult);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("Booking conflict detected! Please choose a different date or quantity.");
        }
    }
}
