using System;
using System.Collections.Generic;
using System.Globalization;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.Services
{
    public class DateService : IDateService
    {
        private readonly CultureInfo _culture;
        private string _format;

        public DateService()
        {
            _format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            _culture = CultureInfo.CurrentCulture;
        }

        public IEnumerable<DateTime> GetAllMonths(DateTime? currentDate = null)
        {
            var current = currentDate ?? DateTime.Now;
            var calendarYear = _culture.Calendar.GetYear(current);
            var firstOfCalendarYear = _culture.Calendar.ToDateTime(calendarYear, 1, 1, 0, 0, 0, 0);
            for (var i = 0; i < _culture.Calendar.GetMonthsInYear(calendarYear); i++)
                yield return _culture.Calendar.AddMonths(firstOfCalendarYear, i);
        }

        public string GetMonthName(DateTime month)
        {
            var calendarMonth = _culture.Calendar.GetMonth(month);
            return _culture.DateTimeFormat.MonthNames[calendarMonth - 1];
        }

        public string GetAbbreviatedMonthName(DateTime month)
        {
            var calendarMonth = _culture.Calendar.GetMonth(month);
            return _culture.DateTimeFormat.AbbreviatedMonthNames[calendarMonth - 1];
        }
    }
}
