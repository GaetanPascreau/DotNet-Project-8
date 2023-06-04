using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppointmentService.Models

{
    public class CHDBContext : IdentityUserContext<IdentityUser>
    {
        public CHDBContext(DbContextOptions<CHDBContext> options)
          : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Consultant> Consultants { get; set; }

        public DbSet<ConsultantCalendar> ConsultantCalendars { get; set; }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}