using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Enums;
using Scheduler.Blazor.EventArgs;
using Scheduler.Blazor.Helpers;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerWeek<T> : MudComponentBase where T : IAppointment
    {
        [Parameter] public DateTime FirstSundayOfWeek { get; set; }
        [Parameter] public EWeekType WeekType { get; set; } = EWeekType.FullWeek;
        [Parameter] public IList<T> Appointments { get; set; }
        [Parameter] public EventCallback<IList<T>> AppointmentsChanged { get; set; }
        [Parameter] public Variant AppointmentVariant { get; set; }
        [Parameter] public TimeSpan AppointmentSpan { get; set; }
        [Parameter] public EScheduleView ScheduleView { get; set; } = EScheduleView.Calendar;
        [Parameter] public DateTime? DayStartTime { get; set; }
        [Parameter] public DateTime? DayEndTime { get; set; }
        [Parameter] public EventCallback<AppointmentClickArgs<T>> OnAppointmentClick { get; set; }
        [Parameter] public EventCallback<DateClickArgs> OnDayClick { get; set; }

        private int DaysInWeek
        {
            get
            {
                return WeekType switch
                {
                    EWeekType.FullWeek => 7,
                    _ => 5
                };
            }
        }

        private DayOfWeek StartDayOfWeek
        {
            get
            {
                return WeekType switch
                {
                    EWeekType.FullWeek => DayOfWeek.Sunday,
                    _ => DayOfWeek.Monday
                };
            }
        }

        private DateTime StartOfWeek
        {
            get
            {
                return WeekType switch
                {
                    EWeekType.FullWeek => FirstSundayOfWeek,
                    _ => FirstSundayOfWeek.GetNext(DayOfWeek.Monday)
                };
            }
        }

        private DateTime EndOfWeek
        {
            get
            {
                return WeekType switch
                {
                    EWeekType.FullWeek => StartOfWeek.AddDays(6),
                    _ => StartOfWeek.AddDays(4)
                };
            }
        }

        private DateTime AdjustTime(DateTime date, DateTime? adjustedDateTime)
        {
            if (adjustedDateTime is null) return date;

            var adjustedDate = (DateTime)adjustedDateTime;
            return new DateTime(date.Year, date.Month, date.Day,
                adjustedDate.Hour, adjustedDate.Minute, adjustedDate.Second);
        }
    }
}
