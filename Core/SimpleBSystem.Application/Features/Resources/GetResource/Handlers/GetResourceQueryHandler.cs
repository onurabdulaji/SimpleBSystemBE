using MediatR;
using SimpleBSystem.Application.Features.Resources.GetResource.Dtos;
using SimpleBSystem.Application.Features.Resources.GetResource.Queries;
using SimpleBSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Resources.GetResource.Handlers
{
    public class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, List<GetResourcesResponseDto>>
    {
        private readonly IResourceRepository _resourceRepository;

        public GetResourceQueryHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<List<GetResourcesResponseDto>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
        {
            var resources = await _resourceRepository.GetAllAsync();

            return resources.Select(r => new GetResourcesResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Quantity = r.Quantity
            }).ToList();
        }
    }
}
