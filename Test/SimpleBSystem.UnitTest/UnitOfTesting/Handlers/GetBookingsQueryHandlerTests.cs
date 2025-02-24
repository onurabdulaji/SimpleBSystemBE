using FluentAssertions;
using Moq;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Handlers;
using SimpleBSystem.Application.Features.Bookings.GetBooking.Query;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Domain.Entities;

namespace SimpleBSystem.UnitTest.UnitOfTesting.Handlers
{
    public class GetBookingsQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly GetBookingsQueryHandler _handler;

        public GetBookingsQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _handler = new GetBookingsQueryHandler(_mockBookingRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsBookings()
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    ResourceId = 1,
                    BookedQuantity = 5,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now.AddHours(2)
                },
                new Booking
                {
                    Id = 2,
                    ResourceId = 2,
                    BookedQuantity = 3,
                    DateFrom = DateTime.Now.AddDays(1),
                    DateTo = DateTime.Now.AddDays(1).AddHours(2)
                }
            };

            _mockBookingRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(bookings);

            var query = new GetBookingsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[0].ResourceId.Should().Be(1);
            result[0].BookedQuantity.Should().Be(5);
            result[0].DateFrom.Should().Be(bookings[0].DateFrom);
            result[0].DateTo.Should().Be(bookings[0].DateTo);

            result[1].Id.Should().Be(2);
            result[1].ResourceId.Should().Be(2);
            result[1].BookedQuantity.Should().Be(3);
            result[1].DateFrom.Should().Be(bookings[1].DateFrom);
            result[1].DateTo.Should().Be(bookings[1].DateTo);

            _mockBookingRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_NoBookings_ReturnsEmptyList()
        {
            _mockBookingRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Booking>());

            var query = new GetBookingsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _mockBookingRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
