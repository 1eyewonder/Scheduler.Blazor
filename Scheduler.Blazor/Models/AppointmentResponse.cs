using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Models
{
    public record AppointmentResponse<T>(T Appointment, bool Cancelled = false, bool Error = false, string Message = null)
        where T : class, IAppointment, new();
}
