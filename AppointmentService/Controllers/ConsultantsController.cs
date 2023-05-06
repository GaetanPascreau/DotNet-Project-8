using AppointmentService.DTOs;
using AppointmentService.Models;
using AppointmentService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppointmentService.DTOs.ConsultantDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantsRepository _consultantsRepository;

        public ConsultantsController(IConsultantsRepository consultantsRepository)
        {
            _consultantsRepository = consultantsRepository;
        }

        // GET: api/Consultants
        [HttpGet]
        public async Task<IEnumerable<ConsultantDto>> GetAsync()
        {
            var appointments = (await _consultantsRepository.GetAllConsultants())
                                .Select(consultant => consultant.ConsultantAsDto());

            return appointments;
        }

        // GET api/Consultants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultantDto>> GetByIdAsync(int id)
        {
            var consultant = await _consultantsRepository.GetSingleConsultant(id);

            if (consultant == null)
            {
                return NotFound();
            }
            return consultant.ConsultantAsDto();
        }

        // POST api/Consultants
        [HttpPost]
        public async Task<ActionResult<ConsultantDto>> PostAsync(CreateConsultantDto createConsultantDto)
        {
            var consultant = new Consultant
            {
                FName = createConsultantDto.FName,
                LName = createConsultantDto.LName,
                Speciality = createConsultantDto.Speciality,
            };

            await _consultantsRepository.CreateConsultant(consultant);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = consultant.Id }, consultant);

        }

        // PUT api/Consultants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateConsultantDto updateConsultantDto)
        {
            // First check if the consultant to update really exists in the db
            var existingConsultant = await _consultantsRepository.GetSingleConsultant(id);

            if (existingConsultant == null)
            {
                return NotFound();
            }

            existingConsultant.FName = updateConsultantDto.FName;
            existingConsultant.LName = updateConsultantDto.LName;
            existingConsultant.Speciality = updateConsultantDto.Speciality;

            await _consultantsRepository.UpdateConsultant(existingConsultant);

            return NoContent();
        }

        // DELETE api/<ConsultantsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var consultantToDelete = await _consultantsRepository.GetSingleConsultant(id);

            if (consultantToDelete == null)
            {
                return NotFound();
            }

            await _consultantsRepository.DeleteConsultant(consultantToDelete.Id);

            return NoContent();
        }
    }
}
