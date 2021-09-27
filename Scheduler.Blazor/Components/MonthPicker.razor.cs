using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class MonthPicker
    {
        [Inject] private IDateService DateService { get; set; }
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public EventCallback<DateTime> SelectedDateChanged { get; set; }
        [Parameter] public EventCallback<DateTime> OnYearSelect { get; set; }
        [Parameter] public Variant Variant { get; set; }
    }
}
