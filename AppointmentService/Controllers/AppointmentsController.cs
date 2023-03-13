using AppointmentService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("Appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public AppointmentsController(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        // GET/appointments
        [HttpGet]
        public async Task<IEnumerable<AppointmentDto>> GetAsync()
        {
            var appointments = (await _appointmentsRepository.GetAllAppointments())
                                .Select(appointment => appointment.AsDto());

            return appointments;
        }

        // GET/appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetByIdAsync(int id)
        {
            // var appointment = appointments.Where(appointment => appointment.Id == id).SingleOrDefault();
            var appointment = await _appointmentsRepository.GetSingleAppointment(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment.AsDto();
        }

        // POST/appointments
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> PostAsync(CreateAppointmentDto createAppointmentDto)
        {
            var enDateTime = createAppointmentDto.StartDateTime.AddMinutes(30);

            var appointment = new Appointment
            {
                StartDateTime = createAppointmentDto.StartDateTime,
                EndDateTime = createAppointmentDto.StartDateTime.AddMinutes(30),
                ConsultantId = createAppointmentDto.ConsultantId,
                PatientId = createAppointmentDto.PatientId
            };

            await _appointmentsRepository.CreateAppointment(appointment);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = appointment.Id }, appointment);
        }

        // PUT/appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            var existingAppointment = await _appointmentsRepository.GetSingleAppointment(id);

            if (existingAppointment == null)
            {
                return NotFound();
            }

            existingAppointment.StartDateTime = updateAppointmentDto.StartDateTime;
            existingAppointment.EndDateTime = updateAppointmentDto.StartDateTime.AddMinutes(30);
            existingAppointment.ConsultantId = updateAppointmentDto.ConsultantId;
            existingAppointment.PatientId = updateAppointmentDto.PatientId;

            await _appointmentsRepository.UpdateAppointment(existingAppointment);

            return NoContent();
        }

        // DELETE/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Appointment = await _appointmentsRepository.GetSingleAppointment(id);

            if (Appointment == null)
            {
                return NotFound();
            }

            await _appointmentsRepository.DeleteAppointment(Appointment.Id);

            return NoContent();
        }
    }
}
