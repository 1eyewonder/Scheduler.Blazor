using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.EventArgs;
using Scheduler.Blazor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Blazor.Components
{
    public partial class AppointmentGrid<T> : MudComponentBase where T : IAppointment
    {
        [Inject] private IDateTimeService DateTimeService { get; set; }

        [Parameter] public List<T> Appointments { get; set; } = new();
        [Parameter] public DateTime Date { get; set; }
        [Parameter] public DateTime? DayStartTime { get; set; }
        [Parameter] public DateTime? DayEndTime { get; set; }
        [Parameter] public EventCallback<AppointmentClickArgs<T>> OnAppointmentClick { get; set; }

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

        protected override async Task OnInitializedAsync()
        {
            _timeCoverage = GetSchedulerTimeSlots();
            _myStyle = "display: grid; " +
                       $"grid-template-rows: repeat({_timeCoverage.Count}, minmax(0, 1fr));" +
                       "grid-template-columns: 1fr 2fr;";
        }

        private string _myStyle;
        private List<DateTime> _timeCoverage = new();

        /// <summary>
        /// Gets the DateTimes for each timespan between the start and end day times
        /// </summary>
        /// <returns></returns>
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
    }
}
