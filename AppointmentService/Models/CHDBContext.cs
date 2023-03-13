using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Models

{
    public class CHDBContext : DbContext
    {
        public CHDBContext(DbContextOptions<CHDBContext> options)
          : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Consultant> Consultants { get; set; }

        public DbSet<ConsultantCalendar> ConsultantCalendars { get; set; }

        public DbSet<Patient> Patients { get; set; }

    }
}