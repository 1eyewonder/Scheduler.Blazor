using Microsoft.Extensions.DependencyInjection;
using Scheduler.Blazor.Interfaces;
using Scheduler.Blazor.Services;

namespace Scheduler.Blazor.Helpers
{
    public static class ServiceProviderExtensions
    {
        public static void AddBlazorScheduler<T>(this IServiceCollection services)
        where T : class, IAppointment, new()
        {
            services.AddScoped<IAppointmentService<T>, AppointmentService<T>>();
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}
