using AppointmentService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("AppointmentAvailability")]
    public class AppointmentAvailabilityController
    {
        private readonly IConsultantCalendarRepository _consultantCalendarRepository;

        public AppointmentAvailabilityController(IConsultantCalendarRepository consultantCalendarRepository)
        {
            _consultantCalendarRepository = consultantCalendarRepository;
        }

        // GET/AppointmentAvailability/{id}
        [HttpGet("{consultantId}")]
        public async Task<IEnumerable<ConsultantCalendar>> GetAvailableAppointmentsByConsultantIdAsync(int consultantId)
        {
            var availableDates = await _consultantCalendarRepository.GetAvailableAppointmentsByConsultantIdAsync(consultantId);

            if (availableDates == null)
            {
                return null;
            }

            return availableDates;
        }
    }
}
