using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthPortal.Models
{
    public class CHDBContext : DbContext
    {
        public DbSet<Appointment> appointments { get; set; }

        public DbSet<Consultant> consultants { get; set; }

        public DbSet<ConsultantCalendar> consultantCalendars { get; set; }

        public DbSet<Patient> patients { get; set; }

    }
}
