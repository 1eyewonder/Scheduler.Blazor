using System;

namespace Scheduler.Blazor.Helpers
{
    public static class Extensions
    {
        public static DateTime GetPrevious(this DateTime dt, 
            DayOfWeek dayOfWeek, 
            bool includeToday = true)
        {
            if (dt.DayOfWeek == dayOfWeek)
            {
                if (includeToday)
                {
                    return dt;
                }
                dt.AddDays(1);
            }
            int diff = (7 + (dt.DayOfWeek - dayOfWeek)) % 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime GetNext(this DateTime dt, 
            DayOfWeek dayOfWeek, 
            bool includeToday = true)
        {
            if (dt.DayOfWeek == dayOfWeek)
            {
                if (includeToday)
                {
                    return dt;
                }
                dt.AddDays(-1);
            }
            int diff = (7 + (dayOfWeek - dt.DayOfWeek)) % 7;
            return dt.AddDays(diff).Date;
        }

        public static bool Overlaps<T>(this (T, T) dt, (T, T) other) 
            where T : IComparable<T> =>
            dt.Item1.CompareTo(other.Item2) <= 0 && 
            other.Item1.CompareTo(dt.Item2) <= 0;

        public static bool Between<T>(this T item, 
            T start, 
            T end, 
            bool inclusive = true) 
            where T : IComparable<T>
        {
            return inclusive
                ? item.CompareTo(start) >= 0 && item.CompareTo(end) <= 0
                : item.CompareTo(start) > 0 && item.CompareTo(end) < 0;
        }
    }
}
