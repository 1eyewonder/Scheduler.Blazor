using Microsoft.AspNetCore.Components.Web;
using System;

namespace Scheduler.Blazor.EventArgs
{
    public class DateClickArgs
    {
        public DateTime Date { get; set; }
        public MouseEventArgs MouseEventArgs { get; set; }
    }
}
