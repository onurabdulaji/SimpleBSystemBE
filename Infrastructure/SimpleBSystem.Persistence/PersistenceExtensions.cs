using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBSystem.Application.CommonHelpers.Interface;
using SimpleBSystem.Application.CommonHelpers.Repository;
using SimpleBSystem.Application.Features.Bookings.CreateBooking.Rules;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Application.Rules;
using SimpleBSystem.Persistence.Behaviors;
using SimpleBSystem.Persistence.Context;
using SimpleBSystem.Persistence.DBSeed;
using SimpleBSystem.Persistence.Repositories;

namespace SimpleBSystem.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IEmailSender, EmailRepository>();

            services.AddScoped<IBookingConflictChecker, BookingConflictChecker>();
            services.AddScoped<IBookingConflictRule, SameDayBookingRule>();
            services.AddScoped<IBookingConflictRule, TimeOverlapRule>();
            services.AddScoped<IBookingConflictRule, ResourceCapacityRule>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AcceptanceHandler<,>));

        }
        public static void UseDatabaseSeeder(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DbSeeder.Seed(context);
        }
    }
}
