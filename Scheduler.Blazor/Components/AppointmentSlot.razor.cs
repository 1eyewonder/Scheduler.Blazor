using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Utilities;
using Scheduler.Blazor.EventArgs;
using Scheduler.Blazor.Helpers;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class AppointmentSlot<T> : MudComponentBase where T : IAppointment
    {
        [Parameter] public T Appointment { get; set; }
        [Parameter] public Variant Variant { get; set; } = Variant.Filled;
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public EventCallback<AppointmentClickArgs<T>> OnClick { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private async Task Click(T appointment, MouseEventArgs e)
        {
            await OnClick.InvokeAsync(new AppointmentClickArgs<T>
            {
                Appointment = appointment, 
                MouseEventArgs = e
            });
        }
    }
}
