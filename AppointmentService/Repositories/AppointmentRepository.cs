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
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            var AppointmentToUpdate = _context.Appointments.Find(appointment.Id);
            if (AppointmentToUpdate is null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            AppointmentToUpdate.StartDateTime = appointment.StartDateTime;
            AppointmentToUpdate.EndDateTime = appointment.EndDateTime;
            AppointmentToUpdate.ConsultantId = appointment.ConsultantId;
            AppointmentToUpdate.PatientId = appointment.PatientId;

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
            await _context.SaveChangesAsync();
        }
    }
}
