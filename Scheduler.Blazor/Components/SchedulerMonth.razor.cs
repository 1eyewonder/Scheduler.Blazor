using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Scheduler.Blazor.Dialogs;
using Scheduler.Blazor.Enums;
using Scheduler.Blazor.EventArgs;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Components
{
    public partial class SchedulerMonth<T> where T : IAppointment
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public EventCallback<DateTime> SelectedDateChanged { get; set; }
        [Parameter] public IList<T> Appointments { get; set; }
        [Parameter] public EventCallback<IList<T>> AppointmentsChanged { get; set; }
        [Parameter] public EWeekType WeekType { get; set; } = EWeekType.FullWeek;
        [Parameter] public EventCallback<EWeekType> WeekTypeChanged { get; set; }
        [Parameter] public Variant AppointmentVariant { get; set; }
        [Parameter] public Variant Variant { get; set; }
        [Parameter] public EventCallback<AppointmentClickArgs<T>> OnAppointmentClick { get; set; }
        [Parameter] public EventCallback<DateClickArgs> OnDayClick { get; set; }

        private DateTime StartOfMonth => new(SelectedDate.Year, SelectedDate.Month, 1);
        private string MonthName => SelectedDate.ToString("MMMM");
        private DayOfWeek FirstDayOfMonth => new DateTime(SelectedDate.Year, SelectedDate.Month, 1).DayOfWeek;
        private int WeeksInMonth => ((int)FirstDayOfMonth + DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month))/6;

        private async Task SelectedMonthDialog()
        {
            var parameters = new DialogParameters
            {
                { "SelectedDate", SelectedDate }
            };

            var dialog = DialogService.Show<SelectMonthDialog>("Select Month", parameters, new DialogOptions
            {
                NoHeader = true,
                FullWidth = true,
                CloseButton = false
            });

            var result = await dialog.Result;
            if (result is not null)
            {
                SelectedDate = (DateTime)result.Data;
            }
        }

        private void SelectToday()
        {
            SelectedDate = DateTime.Now;
        }

        private async Task AddMonthToDate()
        {
            var newDate = new DateTime(SelectedDate.Ticks).AddMonths(1);
            SelectedDate = newDate;
            await SelectedDateChanged.InvokeAsync(newDate);
            StateHasChanged();
        }

        private async Task SubtractMonthFromDate()
        {
            var newDate = new DateTime(SelectedDate.Ticks).AddMonths(-1);
            SelectedDate = newDate;
            await SelectedDateChanged.InvokeAsync(newDate);
            StateHasChanged();
        }
    }
}
