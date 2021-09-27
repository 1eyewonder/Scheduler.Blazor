using System;
using MudBlazor;

namespace Scheduler.Blazor.Interfaces
{
    public interface IAppointment
    {
        string Title { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        Color Color { get; set; }
    }
}
