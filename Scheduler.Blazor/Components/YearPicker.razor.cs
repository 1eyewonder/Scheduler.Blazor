using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Scheduler.Blazor.Components
{
    public partial class YearPicker
    {
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public EventCallback<DateTime> SelectedDateChanged { get; set; }
        [Parameter] public EventCallback<DateTime> OnYearSelect { get; set; }
        [Parameter] public Variant Variant { get; set; }

        private List<DateTime> GetYears()
        {
            List<DateTime> temp = new();
            for (var i = SelectedDate.Year - 10; i <= SelectedDate.Year + 10; i++)
            {
                temp.Add(new DateTime(i, SelectedDate.Month, SelectedDate.Day));
            }

            return temp;
        }

        private async Task SetYear(DateTime year)
        {
            SelectedDate = year;
            await SelectedDateChanged.InvokeAsync(year);
            await OnYearSelect.InvokeAsync(year);
        }
    }
}
