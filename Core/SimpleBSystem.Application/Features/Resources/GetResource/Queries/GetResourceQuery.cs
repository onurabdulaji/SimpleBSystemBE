using MediatR;
using SimpleBSystem.Application.Features.Resources.GetResource.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.Features.Resources.GetResource.Queries
{
    public class GetResourceQuery : IRequest<List<GetResourcesResponseDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
