using AppointmentService.DTOs;
using AppointmentService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppointmentService.DTOs.ConsultantCalendarDtos;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("ConsultantCalendars")]
    public class ConsultantCalendarController : Controller
    {
        private readonly IConsultantCalendarRepository _consultantCalendarRepository;

        public ConsultantCalendarController(IConsultantCalendarRepository consultantCalendarRepository)
        {
            _consultantCalendarRepository = consultantCalendarRepository;
        }

        // GET/consultantCalendars/{id}
        //[HttpGet("{consultantId}")]
        //public async Task<IEnumerable<ConsultantCalendar>> GetAvailableAppointmentsByConsultantIdAsync(int consultantId)
        //{
        //    var availableDates = await _consultantCalendarRepository.GetAvailableAppointmentsByConsultantIdAsync(consultantId);

        //    if (availableDates == null)
        //    {
        //        return (IEnumerable<ConsultantCalendar>)NotFound();
        //    }

        //    return availableDates;
        //}

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
            // First check that the selected date does not already exist ?
            

            var consultantCalendar = new ConsultantCalendar
            {
                ConsultantId = createConsultantCalendarDto.ConsultantId,
                Date = createConsultantCalendarDto.Date,
                Available = createConsultantCalendarDto.Available
            };

            await _consultantCalendarRepository.CreateConsultantCalendar(consultantCalendar);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = consultantCalendar.Id }, consultantCalendar);
        }

        // PUT/consultantCalendars/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateConsultantCalendarDto updateConsultantCalendarDto)
        {
            // First check if the appointment to update really exists in the db
            var existingConsultantCalendar = await _consultantCalendarRepository.GetConsultantCalendarById(id);

            if (existingConsultantCalendar == null)
            {
                return NotFound();
            }

            // Then check whether the selected date is available for the given consultant ?

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
