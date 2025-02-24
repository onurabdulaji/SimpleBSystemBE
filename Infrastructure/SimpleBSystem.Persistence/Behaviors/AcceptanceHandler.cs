using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.Behaviors
{
    public class AcceptanceHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<AcceptanceHandler<TRequest, TResponse>> _logger;

        public AcceptanceHandler(ILogger<AcceptanceHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {Request}", typeof(TRequest).Name);

            var response = await next();

            _logger.LogInformation("{Request} handled successfully", typeof(TRequest).Name);

            return response;
        }
    }
}
