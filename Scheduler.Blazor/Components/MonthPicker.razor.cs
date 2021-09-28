using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class MonthPicker
    {
        [Inject] private IDateTimeService DateTimeService { get; set; }
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public EventCallback<DateTime> SelectedDateChanged { get; set; }
        [Parameter] public EventCallback<DateTime> OnYearSelect { get; set; }
        [Parameter] public Variant Variant { get; set; }

        private async Task SetMonth(DateTime month)
        {
            SelectedDate = month;
            await SelectedDateChanged.InvokeAsync(month);
        }

        private async Task AddYearToDate()
        {
            var newDate = new DateTime(SelectedDate.Ticks).AddYears(1);
            SelectedDate = newDate;
            await SelectedDateChanged.InvokeAsync(newDate);
            StateHasChanged();
        }

        private async Task SubtractYearFromDate()
        {
            var newDate = new DateTime(SelectedDate.Ticks).AddYears(-1);
            SelectedDate = newDate;
            await SelectedDateChanged.InvokeAsync(newDate);
            StateHasChanged();
        }
    }
}
