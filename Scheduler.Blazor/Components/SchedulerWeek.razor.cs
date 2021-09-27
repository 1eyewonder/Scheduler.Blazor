using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Enums;
using Scheduler.Blazor.Helpers;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerWeek<T> where T : IAppointment
    {
        [Parameter] public DateTime FirstSundayOfWeek { get; set; }
        [Parameter] public EWeekType WeekType { get; set; } = EWeekType.FullWeek;
        [Parameter] public IList<IAppointment> Appointments { get; set; }
        [Parameter] public Variant AppointmentVariant { get; set; }

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
    }
}
