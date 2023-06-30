using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentService.Models
{
    public class ConsultantCalendarModel
    {
        public int id { get; set; }
        public string consultantName { get; set; }

        public List<DateTime> availableDates { get; set; }

        // Tracking property to handle concurrency conflicts at the time of appointment booking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}