using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AppointmentService.Repositories
{
    public interface IConsultantsRepository
    {
        Task<List<Consultant>> GetAllConsultants();

        Task<Consultant> GetSingleConsultant(int id);

        Task CreateConsultant(Consultant consultant);

        Task UpdateConsultant(Consultant consultant);

        Task DeleteConsultant(int Id);
    }
}
