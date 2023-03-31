
using AppointmentService.DTOs;
using static AppointmentService.DTOs.ConsultantCalendarDtos;

namespace AppointmentService
{
    public static class Extensions
    {
        public static AppointmentDto AppointmentAsDto(this Appointment appointment)
        {
            return new AppointmentDto(appointment.Id, appointment.StartDateTime, appointment.EndDateTime, appointment.ConsultantId, appointment.PatientId);
        }

        public static ConsultantCalendarDto ConsultantCalendarAsDto(this ConsultantCalendar consultantCalendar)
        {
            return new ConsultantCalendarDto(consultantCalendar.Id, consultantCalendar.ConsultantId, consultantCalendar.Date, consultantCalendar.Available);
        }
    }
}
