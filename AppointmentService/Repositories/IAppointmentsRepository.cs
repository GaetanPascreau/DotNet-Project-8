using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointment>> GetAllAppointments();

        Task<Appointment> GetSingleAppointment(int id);

        Task CreateAppointment(Appointment appointment);

        Task UpdateAppointment(Appointment appointment);

        Task DeleteAppointment(int Id);

        // Check all available Dates in a consultant calendar
        Task<List<ConsultantCalendar>> GetAvailableDatesByConsultant(int consultantId);

        // Check if a specified date is available for a specified consultant
        Task<bool> IsAvailableDateforConsultant(int consultantId, DateTime startDateTime);
    }
}
