using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Models
{
    public record AppointmentResponse<T>(T Appointment, bool Cancelled)
        where T : class, IAppointment, new();
}
