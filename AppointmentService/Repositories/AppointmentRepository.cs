using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentService.Repositories
{
    public class AppointmentRepository : IAppointmentsRepository
    {
        private readonly CHDBContext _context;

        public AppointmentRepository(CHDBContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return appointments;
        }

        public async Task<Appointment> GetSingleAppointment(int id)
        {
            var appointment = await _context.Appointments.SingleOrDefaultAsync(app => app.Id == id);

            if (appointment == null)
            {
                return null;
            }

            return appointment;
        }

        public async Task CreateAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            _context.Appointments.Add(appointment);

            // Set Available to false in the ConsultantCalendar table for this date and this consultant
            var dateToCheck = await _context.ConsultantCalendars.FirstOrDefaultAsync(consultantCalendar => consultantCalendar.ConsultantId == appointment.ConsultantId && consultantCalendar.Date == appointment.StartDateTime);
            dateToCheck.Available = false;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            _context.ChangeTracker.Clear();

            var appointmentToUpdate = await _context.Appointments.FirstOrDefaultAsync(app => app.Id == appointment.Id);

            if (appointmentToUpdate is null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            // if the appointment's date was modified, set the Available field to true for the original date, and to false for the new date
            if(appointment.StartDateTime != appointmentToUpdate.StartDateTime)
            {
                var originalAppointmentDate = await _context.ConsultantCalendars.FirstOrDefaultAsync(consultantCalendar => consultantCalendar.ConsultantId == appointmentToUpdate.ConsultantId && consultantCalendar.Date == appointmentToUpdate.StartDateTime);
                originalAppointmentDate.Available = true;

                var newAppointmentDate = await _context.ConsultantCalendars.FirstOrDefaultAsync(consultantCalendar => consultantCalendar.ConsultantId == appointment.ConsultantId && consultantCalendar.Date == appointment.StartDateTime);
                newAppointmentDate.Available = false;

                await _context.SaveChangesAsync();
            }

            appointmentToUpdate.StartDateTime = appointment.StartDateTime;
            appointmentToUpdate.EndDateTime = appointment.EndDateTime;
            appointmentToUpdate.ConsultantId = appointment.ConsultantId;
            appointmentToUpdate.PatientId = appointment.PatientId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointment(int id)
        {
            Appointment appointmentToDelete = _context.Appointments.Find(id);

            if (appointmentToDelete is null)
            {
                throw new ArgumentNullException(nameof(appointmentToDelete));
            }

            _context.Appointments.Remove(appointmentToDelete);

            // Set Available to true in the ConsultantCalendar table for this date and this consultant
            var dateToCheck = await _context.ConsultantCalendars.FirstOrDefaultAsync(consultantCalendar => consultantCalendar.ConsultantId == appointmentToDelete.ConsultantId && consultantCalendar.Date == appointmentToDelete.StartDateTime);
            dateToCheck.Available = true;

            await _context.SaveChangesAsync();
        }

        // Get all Available dates for a consultant
        public async Task<List<ConsultantCalendar>> GetAvailableDatesByConsultant(int consultantId)
        {
            var availableAppointments = await _context.ConsultantCalendars.FromSqlRaw("SELECT * FROM [dbo].[ConsultantCalendar] WHERE ConsultantId = {0} and Available = 1", consultantId).ToListAsync();
            return availableAppointments;
        }

        // Check availability of a specified date for a specified consultant
        public async Task<bool> IsAvailableDateforConsultant(int consultantId, DateTime startDateTime)
        {
            var dateToCheck = await _context.ConsultantCalendars.FirstOrDefaultAsync(consultantCalendar => consultantCalendar.ConsultantId == consultantId && consultantCalendar.Date == startDateTime);
            if(dateToCheck == null || dateToCheck.Available == false)
            {
                return false;
            }
            return true;
        }
    }
}
