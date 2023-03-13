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
    }
}
