using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Enums;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerMonth<T> where T : IAppointment
    {
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public EventCallback<DateTime> SelectedDateChanged { get; set; }
        [Parameter] public IList<IAppointment> Appointments { get; set; }
        [Parameter] public EWeekType WeekType { get; set; } = EWeekType.FullWeek;
        [Parameter] public EventCallback<EWeekType> WeekTypeChanged { get; set; }
        [Parameter] public Variant AppointmentVariant { get; set; }
        [Parameter] public Variant Variant { get; set; }

        private DateTime StartOfMonth => new(SelectedDate.Year, SelectedDate.Month, 1);
        private DateTime EndOfMonth => StartOfMonth.AddMonths(1).AddDays(-1);
        private string MonthName => SelectedDate.ToString("MMMM");
        private DayOfWeek FirstDayOfMonth => new DateTime(SelectedDate.Year, SelectedDate.Month, 1).DayOfWeek;
        private int WeeksInMonth => ((int)FirstDayOfMonth + DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month))/6;
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
    }
}
