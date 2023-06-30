using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentService.Repositories
{
    public class ConsultantRepository : IConsultantsRepository
    {
        private readonly CHDBContext _context;

        public ConsultantRepository(CHDBContext context)
        {
            _context = context;
        }

        public async Task<List<Consultant>> GetAllConsultants()
        {
            var consultants = await _context.Consultants.ToListAsync();
            return consultants;
        }

        public async Task<Consultant> GetSingleConsultant(int id)
        {
            var consultant = await _context.Consultants.SingleOrDefaultAsync(c => c.Id == id);

            if (consultant == null)
            {
                return null;
            }

            return consultant;
        }

        public async Task CreateConsultant(Consultant consultant)
        {
            if (consultant == null)
            {
                throw new ArgumentNullException(nameof(consultant));
            }

            await _context.AddAsync(consultant);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateConsultant(Consultant consultant)
        {
            _context.ChangeTracker.Clear();

            var consultantToUpdate = await _context.Consultants.FirstOrDefaultAsync(app => app.Id == consultant.Id);

            if (consultantToUpdate is null)
            {
                throw new ArgumentNullException(nameof(consultant));
            }

            consultantToUpdate.LName = consultant.LName;
            consultantToUpdate.FName = consultant.FName;
            consultantToUpdate.Speciality = consultant.Speciality;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteConsultant(int id)
        {
            var consultantToDelete = await _context.Consultants.SingleOrDefaultAsync(c => c.Id == id);

            if (consultantToDelete is null)
            {
                throw new ArgumentNullException(nameof(consultantToDelete));
            }

            _context.Consultants.Remove(consultantToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
