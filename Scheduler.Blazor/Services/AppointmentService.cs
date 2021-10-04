using MudBlazor;
using Scheduler.Blazor.Dialogs;
using Scheduler.Blazor.Interfaces;
using Scheduler.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Scheduler.Blazor.Helpers;

namespace Scheduler.Blazor.Services
{
    public class AppointmentService<T> : IAppointmentService<T>
        where T : class, IAppointment, new()
    {
        private readonly IDialogService _dialogService;

        private static DialogOptions Options => new()
        {
            FullWidth = true
        };

        public AppointmentService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<AppointmentResponse<T>> CreateAppointment(IList<T> scheduledAppointments,
            DateTime initialDate, DialogOptions options = null)
        {
            var parameters = new DialogParameters
            {
                { "ScheduledAppointments", scheduledAppointments },
                {
                    "Appointment",
                    new Appointment
                    {
                        Title = "Appointment",
                        Color = Color.Primary,
                        Start = initialDate.Date + new TimeSpan(8, 0, 0),
                        End = initialDate.Date + new TimeSpan(8, 30, 0)
                    }
                }
            };

            var dialog = _dialogService.Show<CreateAppointmentDialog<T>>("Create Appointment",
                parameters,
                options ?? Options);

            var result = await dialog.Result;
            return result.Cancelled
                ? new AppointmentResponse<T>(null, true)
                : (AppointmentResponse<T>)result.Data;
        }

        public async Task<AppointmentResponse<T>> EditAppointment(IList<T> scheduledAppointments,
            IAppointment appointment, DialogOptions options = null)
        {
            var parameters = new DialogParameters();
            var dtoAppointment = new Appointment
            {
                Title = appointment.Title,
                Color = appointment.Color,
                Start = appointment.Start,
                End = appointment.End
            };

            parameters.Add("Appointment", dtoAppointment);
            parameters.Add("ScheduledAppointments", scheduledAppointments);

            var dialog = _dialogService.Show<EditAppointmentDialog<T>>(
                "Edit Appointment",
                parameters,
                options ?? Options);

            var result = await dialog.Result;
            return result.Cancelled
                ? new AppointmentResponse<T>(null, true)
                : (AppointmentResponse<T>)result.Data;
        }
    }
}
