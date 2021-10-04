using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Enums;
using Scheduler.Blazor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Utilities;
using Scheduler.Blazor.EventArgs;
using Scheduler.Blazor.Helpers;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerDay<T> where T : IAppointment
    {
        [Parameter] public DateTime Date { get; set; }
        [Parameter] public IList<T> Appointments { get; set; }
        [Parameter] public EventCallback<IList<T>> AppointmentsChanged { get; set; }
        [Parameter] public int MaxVisibleAppointments { get; set; } = 5;
        [Parameter] public Variant AppointmentVariant { get; set; } = Variant.Filled;
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public EScheduleView ScheduleView { get; set; } = EScheduleView.Calendar;
        [Parameter] public DateTime? DayStartTime { get; set; }
        [Parameter] public DateTime? DayEndTime { get; set; }
        [Parameter] public EventCallback<AppointmentClickArgs<T>> OnAppointmentClick { get; set; }
        [Parameter] public EventCallback<DateClickArgs> OnDayClick { get; set; }

        [Inject] private IDateTimeService DateTimeService { get; set; }

        private TimeSpan _appointmentSpan;

        [Parameter]
        public TimeSpan AppointmentSpan
        {
            get => _appointmentSpan;
            set
            {
                if (AppointmentSpan == value) return;
                if (value.Minutes % 5 != 0)
                {
                    throw new ArgumentException("Appointment span must be divisible by 5 minutes",
                        nameof(AppointmentSpan));
                }

                _appointmentSpan = value;
            }
        }

        private bool _initialized;

        protected override void OnInitialized()
        {
            _initialized = true;
        }

        private List<DateTime> GetSchedulerTimeSlots()
        {
            List<DateTime> timeCoverage;
            if (DayStartTime is not null && DayEndTime is not null)
            {
                timeCoverage = DateTimeService
                    .EveryXMinutesInTimeSpan(AppointmentSpan,
                        (DateTime)DayStartTime,
                        (DateTime)DayEndTime)
                    .ToList();
            }
            else if (DayStartTime is not null && DayEndTime is null)
            {
                timeCoverage = DateTimeService
                    .EveryXMinutesInTimeSpan(AppointmentSpan,
                        (DateTime)DayStartTime,
                        Date.Date.AddDays(1).AddTicks(-1))
                    .ToList();
            }
            else if (DayStartTime is null && DayEndTime is not null)
            {
                timeCoverage = DateTimeService
                    .EveryXMinutesInTimeSpan(AppointmentSpan,
                        Date.Date,
                        (DateTime)DayEndTime)
                    .ToList();
            }
            else
            {
                timeCoverage = DateTimeService
                    .EveryXMinutesInDay(AppointmentSpan, Date)
                    .ToList();
            }

            return timeCoverage;
        }

        private async Task Click(MouseEventArgs e)
        {
            await OnDayClick.InvokeAsync(new DateClickArgs
            {
                Date = Date,
                MouseEventArgs = e
            });
        }
    }
}
