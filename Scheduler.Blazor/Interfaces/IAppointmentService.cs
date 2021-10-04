using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MudBlazor;
using Scheduler.Blazor.Models;

namespace Scheduler.Blazor.Interfaces
{
    public interface IAppointmentService<T> where T : class, IAppointment, new()
    {
        /// <summary>
        /// Opens a 'Create Appointment' dialog
        /// </summary>
        /// /// <param name="scheduledAppointments">Scheduled appointments to check for conflicts</param>
        /// <param name="initialDate">The date the appointment defaults to on open</param>
        /// <param name="options">Dialog options</param>
        /// <returns></returns>
        Task<AppointmentResponse<T>> CreateAppointment(IList<T> scheduledAppointments, DateTime initialDate, DialogOptions options = null);

        /// <summary>
        /// Opens an 'Edit Appointment' dialog
        /// </summary>
        /// <param name="scheduledAppointments">Scheduled appointments to check for conflicts</param>
        /// <param name="appointment">Appointment to edit</param>
        /// <param name="options">Dialog options</param>
        /// <returns></returns>
        Task<AppointmentResponse<T>> EditAppointment(IList<T> scheduledAppointments, IAppointment appointment, DialogOptions options = null);
    }
}
