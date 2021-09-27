using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerDay<T> where T : IAppointment
    {
        [Parameter] public DateTime Date { get; set; }
        [Parameter] public IList<IAppointment> Appointments { get; set; }
        [Parameter] public int MaxVisibleAppointments { get; set; } = 5;
        [Parameter] public Variant AppointmentVariant { get; set; } = Variant.Filled;

        //private bool IsDiffMonth => Day.Month != Scheduler.CurrentDate.Month;
        //private string DateText => (IsDiffMonth && Day.Day == 1) ? Day.ToString("MMM d") : Day.Day.ToString();
    }
}
