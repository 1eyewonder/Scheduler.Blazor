using System;
using System.Collections.Generic;

namespace Scheduler.Blazor.Interfaces
{
    public interface IDateTimeService
    {
        IEnumerable<DateTime> GetAllMonths(DateTime? currentDate = null);
        string GetMonthName(DateTime month);
        string GetAbbreviatedMonthName(DateTime month);
        IEnumerable<DateTime> EveryXMinutesInDay(TimeSpan span, DateTime startTime);
        IEnumerable<DateTime> EveryXMinutesInTimeSpan(TimeSpan span, DateTime start, DateTime end);
    }
}
