using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Scheduler.Blazor.Dialogs
{
    public partial class SelectMonthDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public DateTime SelectedDate { get; set; }

        private OpenTo _openTo = OpenTo.Month;

        private void SelectMonth()
        {
            MudDialog.Close(DialogResult.Ok(SelectedDate));
        }

        private void ShowYear(DateTime year)
        {
            _openTo = OpenTo.Year;
        }

        private void ShowMonth(DateTime month)
        {
            _openTo = OpenTo.Month;
        }
    }
}
