using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.Repositories
{
    public interface IConsultantCalendarRepository
    {
        // Check all available Dates in a consultant calendar
        Task<List<ConsultantCalendar>> GetAllConsultantCalendars();
        Task<List<ConsultantCalendar>> GetConsultantCalendarsByConsultantId(int consultantId);
        Task<ConsultantCalendar> GetConsultantCalendarById(int id);
        Task<IEnumerable<ConsultantCalendar>> GetAvailableAppointmentsByConsultantIdAsync(int consultantId);
        Task CreateConsultantCalendar(ConsultantCalendar consultantCalendar);
        Task UpdateConsultantCalendar(ConsultantCalendar consultantCalendar);
        Task DeleteConsultantCalendar(int id);
    }
}
