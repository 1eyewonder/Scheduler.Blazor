using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Scheduler.Blazor.Interfaces;

namespace Scheduler.Blazor.EventArgs
{
    public class AppointmentClickArgs<T> where T : IAppointment
    {
        public T Appointment { get; set; }
        public MouseEventArgs MouseEventArgs { get; set; }
    }
}
