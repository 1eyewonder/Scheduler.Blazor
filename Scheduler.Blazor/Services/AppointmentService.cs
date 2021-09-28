using MudBlazor;
using Scheduler.Blazor.Dialogs;
using Scheduler.Blazor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Blazor.Models;

namespace Scheduler.Blazor.Services
{
    public class AppointmentService<T> : IAppointmentService<T>
        where T : class, IAppointment, new()
    {
        private readonly IDialogService _dialogService;

        public AppointmentService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<AppointmentResponse<T>> CreateAppointment(IList<T> scheduledAppointments,
            DateTime initialDate,
            DialogParameters dialogParameters)
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

            if (dialogParameters is not null)
            {
                foreach (var (key, value) in dialogParameters)
                {
                    parameters.Add(key, value);
                }
            }

            var dialog = _dialogService.Show<AppointmentDialog<T>>("Edit Appointment",
                parameters,
                new DialogOptions
                {
                    FullWidth = true
                });

            var result = await dialog.Result;

            return result.Cancelled 
                ? new AppointmentResponse<T>(null, true) 
                : new AppointmentResponse<T>((T)result.Data, false);
        }

        public async Task<AppointmentResponse<T>> EditAppointment(IList<T> scheduledAppointments,
            IAppointment appointment,
            DialogParameters dialogParameters)
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

            if (dialogParameters is not null)
            {
                foreach (var (key, value) in dialogParameters)
                {
                    parameters.Add(key, value);
                }
            }

            var dialog = _dialogService.Show<AppointmentDialog<T>>(
                "Edit Appointment",
                parameters,
                new DialogOptions
                {
                    FullWidth = true
                });

            var result = await dialog.Result;

            return result.Cancelled 
                ? new AppointmentResponse<T>(null, true) 
                : new AppointmentResponse<T>((T)result.Data, false);
        }
    }
}
