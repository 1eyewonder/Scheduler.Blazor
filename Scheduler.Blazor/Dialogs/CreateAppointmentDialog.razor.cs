using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Blazor.Helpers;
using Scheduler.Blazor.Models;

namespace Scheduler.Blazor.Dialogs
{
    public partial class CreateAppointmentDialog<T> where T : class, IAppointment, new()
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public T Appointment { get; set; }
        [Parameter] public IList<T> ScheduledAppointments { get; set; }
        [Parameter] public bool AllowOverlap { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Appointment ??= new T
            {
                Title = "Appointment",
                Color = Color.Primary,
                Start = DateTime.Today + new TimeSpan(8, 0, 0),
                End = DateTime.Today + new TimeSpan(8, 30, 0)
            };
        }

        private void ConfirmAppointment()
        {
            if (Appointment.Start > Appointment.End)
            {
                var response = new AppointmentResponse<T>(null,
                    false,
                    true,
                    "Appointment cannot have an end time occur before the start time");

                MudDialog.Close(DialogResult.Ok(response));
                return;
            }

            if (!AllowOverlap && 
                ScheduledAppointments.Any(x => 
                    (Appointment.Start, Appointment.End)
                    .Overlaps((x.Start, x.End), false)))
            {
                var overlappingAppointments = string.Join(", ",
                        ScheduledAppointments
                            .Where(x => (Appointment.Start, Appointment.End)
                                .Overlaps((x.Start, x.End), false) &&
                                x.Title != Appointment.Title)
                            .Select(x => x.Title));

                var response = new AppointmentResponse<T>(null, 
                    false, 
                    true, 
                    $"{Appointment.Title} overlaps with the following appointments: {overlappingAppointments}");

                MudDialog.Close(DialogResult.Ok(response));
                return;
            }

            MudDialog.Close(DialogResult.Ok(new AppointmentResponse<T>(Appointment)));
        }

        private void CancelCreation()
        {
            MudDialog.Cancel();
        }

        private void UpdateStartTime(TimeSpan? time)
        {
            if (time is null) return;
            Appointment.Start = new DateTime(Appointment.Start.Date.Ticks) +
                                time.Value;
        }

        private void UpdateEndTime(TimeSpan? time)
        {
            if (time is null) return;
            Appointment.End = new DateTime(Appointment.End.Date.Ticks) +
                                time.Value;
        }

        private void UpdateDate(DateTime? date)
        {
            if (date is null) return;

            Appointment.Start = new DateTime(date.Value.Date.Ticks) +
                                Appointment.Start.TimeOfDay;

            Appointment.End = new DateTime(date.Value.Date.Ticks) +
                                Appointment.End.TimeOfDay;
        }
    }
}

