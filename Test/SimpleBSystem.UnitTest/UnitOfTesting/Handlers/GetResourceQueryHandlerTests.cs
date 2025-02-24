using FluentAssertions;
using Moq;
using SimpleBSystem.Application.Features.Resources.GetResource.Handlers;
using SimpleBSystem.Application.Features.Resources.GetResource.Queries;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Domain.Entities;

namespace SimpleBSystem.UnitTest.UnitOfTesting.Handlers
{
    public class GetResourceQueryHandlerTests
    {
        private readonly Mock<IResourceRepository> _mockResourceRepository;
        private readonly GetResourceQueryHandler _handler;

        public GetResourceQueryHandlerTests()
        {
            _mockResourceRepository = new Mock<IResourceRepository>();
            _handler = new GetResourceQueryHandler(_mockResourceRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsResources()
        {
            var resources = new List<Resource>
            {
                new Resource
                {
                    Id = 1,
                    Name = "Resource 1",
                    Quantity = 10
                },
                new Resource
                {
                    Id = 2,
                    Name = "Resource 2",
                    Quantity = 5
                }
            };

            _mockResourceRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(resources);

            var query = new GetResourceQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be("Resource 1");
            result[0].Quantity.Should().Be(10);

            result[1].Id.Should().Be(2);
            result[1].Name.Should().Be("Resource 2");
            result[1].Quantity.Should().Be(5);

            _mockResourceRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_NoResources_ReturnsEmptyList()
        {
            _mockResourceRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Resource>());

            var query = new GetResourceQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _mockResourceRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
