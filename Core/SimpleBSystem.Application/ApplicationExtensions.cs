using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Validators;
using System.Reflection;

namespace SimpleBSystem.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateBookingCommandValidator>();
            return services;
        }
    }
}
