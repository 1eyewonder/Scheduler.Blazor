using System;
using System.Collections.Generic;

namespace Scheduler.Blazor.Interfaces
{
    public interface IDateService
    {
        IEnumerable<DateTime> GetAllMonths(DateTime? currentDate = null);
        string GetMonthName(DateTime month);
        string GetAbbreviatedMonthName(DateTime month);
    }
}
