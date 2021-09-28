using Microsoft.AspNetCore.Components;
using System;
using MudBlazor;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerDayHeader : MudComponentBase
    {
        [Parameter] public DateTime Date { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool Outlined { get; set; }

        private string GetClassThemeColor()
        {
            return Color switch
            {
                Color.Secondary => "mud-theme-secondary",
                Color.Tertiary => "mud-theme-tertiary",
                _ => "mud-theme-primary"
            };
        }
    }
}
