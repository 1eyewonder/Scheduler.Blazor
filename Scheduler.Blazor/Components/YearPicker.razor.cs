using System;
using System.Collections.Generic;
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
            for (var i = SelectedDate.Year; i <= SelectedDate.Year + 10; i++)
            {
                temp.Add(new DateTime(i, SelectedDate.Month, SelectedDate.Day));
            }

            return temp;
        }
            
    }
}
