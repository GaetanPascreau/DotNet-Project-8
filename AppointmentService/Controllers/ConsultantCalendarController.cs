using AppointmentService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppointmentService.DTOs.ConsultantCalendarDtos;

namespace AppointmentService.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("ConsultantCalendars")]
    public class ConsultantCalendarController : Controller
    {
        private readonly IConsultantCalendarRepository _consultantCalendarRepository;

        public ConsultantCalendarController(IConsultantCalendarRepository consultantCalendarRepository)
        {
            _consultantCalendarRepository = consultantCalendarRepository;
        }

        // GET/consultantCalendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultantCalendarDto>>> GetAsync()
        {
            var consultantCalendars = (await _consultantCalendarRepository.GetAllConsultantCalendars())
                                .Select(consultantCalendar => consultantCalendar.ConsultantCalendarAsDto());

            return Ok(consultantCalendars);
        }

        // GET/consultantCalendars/{consultantId}
        [HttpGet("/ConsultantCalendars/Consultant/{consultantId}")]
        public async Task<ActionResult<IEnumerable<ConsultantCalendarDto>>> GetByConsultantIdAsync(int consultantId)
        {
            var consultantCalendar = (await _consultantCalendarRepository.GetConsultantCalendarsByConsultantId(consultantId))
                .Select(consCal => consCal.ConsultantCalendarAsDto());

            if (consultantCalendar == null)
            {
                return NotFound();
            }

            return Ok(consultantCalendar);
        }

        // GET/consultantCalendars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultantCalendarDto>> GetByIdAsync(int id)
        {
            var consultantCalendar = await _consultantCalendarRepository.GetConsultantCalendarById(id);

            if (consultantCalendar == null)
            {
                return NotFound();
            }

            return Ok(consultantCalendar.ConsultantCalendarAsDto());
        }

        // POST/consultantCalendars
        [HttpPost]
        public async Task<ActionResult<ConsultantCalendarDto>> PostAsync(CreateConsultantCalendarDto createConsultantCalendarDto)
        {          
            var consultantCalendar = new ConsultantCalendar
            {
                ConsultantId = createConsultantCalendarDto.ConsultantId,
                Date = createConsultantCalendarDto.Date,
                Available = createConsultantCalendarDto.Available
            };

            await _consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = consultantCalendar.Id }, consultantCalendar);
        }

        // PUT/consultantCalendars/{id} => modified version to handle conurrency conflicts
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateConsultantCalendarDto updateConsultantCalendarDto)
        {
            // First check if the appointment to update really exists in the db
            var existingConsultantCalendar = await _consultantCalendarRepository.GetConsultantCalendarById(id);

            if (existingConsultantCalendar == null)
            {
                return NotFound();
            }
            // Then check whether the RowVersion is still the same since the user has requested the calendar
            byte[] originalRowVersion = Convert.FromBase64String(updateConsultantCalendarDto.RowVersion);

            if(!existingConsultantCalendar.RowVersion.SequenceEqual(originalRowVersion))
            {
                ModelState.AddModelError(string.Empty, "Sorry, " +
                    "this schedule is not available anymore. Please select another one.");
                return View();
            }

            existingConsultantCalendar.ConsultantId = updateConsultantCalendarDto.ConsultantId;
            existingConsultantCalendar.Date = updateConsultantCalendarDto.Date;
            existingConsultantCalendar.Available = updateConsultantCalendarDto.Available;


            await _consultantCalendarRepository.UpdateConsultantCalendar(existingConsultantCalendar);

            return NoContent();
        }

        // DELETE/consultantCalendars/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var consultantCalendar = await _consultantCalendarRepository.GetConsultantCalendarById(id);

            if (consultantCalendar == null)
            {
                return NotFound();
            }

            await _consultantCalendarRepository.DeleteConsultantCalendar(consultantCalendar.Id);

            return NoContent();
        }
    }
}
