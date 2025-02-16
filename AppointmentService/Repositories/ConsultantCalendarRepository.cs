﻿using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.Repositories
{
    public class ConsultantCalendarRepository : IConsultantCalendarRepository
    {
        private readonly CHDBContext _context;

        public ConsultantCalendarRepository(CHDBContext context)
        {
            _context = context;
        }

        public async Task<List<ConsultantCalendar>> GetAllConsultantCalendars()
        {
            var consultantCalendars = await _context.ConsultantCalendars.AsNoTracking().ToListAsync();
            return consultantCalendars;
        }

        public async Task<List<ConsultantCalendar>> GetConsultantCalendarsByConsultantId(int consultantId)
        {
            var consultantCalendars = await _context.ConsultantCalendars.AsNoTracking().Where(c => c.ConsultantId == consultantId).ToListAsync();

            if (consultantCalendars == null)
            {
                return null;
            }

            return consultantCalendars;
        }

        public async Task<ConsultantCalendar> GetConsultantCalendarById(int id)
        {
            var consultantCalendar = await _context.ConsultantCalendars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (consultantCalendar == null)
            {
                return null;
            }

            return consultantCalendar;
        }

        public async Task<IEnumerable<ConsultantCalendar>> GetAvailableAppointmentsByConsultantIdAsync(int consultantId)
        {
            var availableAppointments = await _context.ConsultantCalendars.FromSqlRaw(
                "SELECT * FROM [dbo].[ConsultantCalendar] WHERE ConsultantId = {0} and Available = 1", consultantId).ToListAsync();
            return availableAppointments;
        }

        public async Task CreateConsultantCalendar(ConsultantCalendar consultantCalendar)
        {
            if (consultantCalendar == null)
            {
                throw new ArgumentNullException(nameof(consultantCalendar));
            }

            _context.ConsultantCalendars.Add(consultantCalendar);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateConsultantCalendar(ConsultantCalendar consultantCalendar)
        {
            var consultantCalendarToUpdate = await _context.ConsultantCalendars.FirstOrDefaultAsync(app => app.Id == consultantCalendar.Id);

            if (consultantCalendarToUpdate is null)
            {
                throw new ArgumentNullException(nameof(consultantCalendar));
            }

            consultantCalendarToUpdate.ConsultantId = consultantCalendar.Id;
            consultantCalendarToUpdate.Date = consultantCalendar.Date;
            consultantCalendarToUpdate.Available = consultantCalendar.Available;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteConsultantCalendar(int id)
        {
            ConsultantCalendar consultantCalendarToDelete = _context.ConsultantCalendars.Find(id);

            if (consultantCalendarToDelete is null)
            {
                throw new ArgumentNullException(nameof(consultantCalendarToDelete));
            }

            _context.ConsultantCalendars.Remove(consultantCalendarToDelete);

            await _context.SaveChangesAsync();
        }  
    }
}
