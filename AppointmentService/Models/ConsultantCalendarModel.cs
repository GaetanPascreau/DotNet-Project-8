using System;
using System.Collections.Generic;

namespace AppointmentService.Models
{
    public class ConsultantCalendarModel
    {
        public int id { get; set; }
        public string consultantName { get; set; }

        public List<DateTime> availableDates { get; set; }
    }
}