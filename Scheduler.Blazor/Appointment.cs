using System;
using MudBlazor;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor
{
    public class Appointment : IAppointment
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Color Color { get; set; } = Color.Primary;
    }
}
