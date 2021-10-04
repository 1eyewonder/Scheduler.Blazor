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
    public partial class EditAppointmentDialog<T> where T : class, IAppointment, new()
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public T Appointment { get; set; }
        [Parameter] public IList<T> ScheduledAppointments { get; set; }
        [Parameter] public bool EndTimeEditable { get; set; }
        [Parameter] public bool AllowOverlap { get; set; }

        private TimeSpan _originalAppointmentSpan;
        private IAppointment _originalAppointment;

        protected override async Task OnInitializedAsync()
        {
            Appointment ??= new T
            {
                Title = "Appointment",
                Color = Color.Primary,
                Start = DateTime.Today + new TimeSpan(8, 0, 0),
                End = DateTime.Today + new TimeSpan(8, 30, 0)
            };

            if (!EndTimeEditable)
            {
                _originalAppointmentSpan = new TimeSpan(Appointment.End.TimeOfDay.Ticks) -
                                           new TimeSpan(Appointment.Start.TimeOfDay.Ticks);
            }

            _originalAppointment = new Appointment
            {
                Color = Appointment.Color,
                Title = Appointment.Title,
                Start = Appointment.Start,
                End = Appointment.End
            };
        }

        private void ConfirmAppointment()
        {
            var tempList = new List<T>();
            tempList.AddRange(ScheduledAppointments);

            if (_originalAppointment is not null)
            {
                tempList.RemoveAll(x => x.Title.Equals(_originalAppointment.Title) &&
                                                  x.Start == _originalAppointment.Start);
            }

            if (!AllowOverlap && 
                tempList.Any(x => 
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

        private void UpdateStartTime(TimeSpan? time)
        {
            if (time is null) return;
            Appointment.Start = new DateTime(Appointment.Start.Date.Ticks) +
                                time.Value;

            if (!EndTimeEditable)
            {
                Appointment.End = Appointment.Start + _originalAppointmentSpan;
            }
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
