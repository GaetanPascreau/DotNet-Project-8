
namespace AppointmentService
{
    public static class Extensions
    {
        public static AppointmentDto AsDto(this Appointment appointment)
        {
            return new AppointmentDto(appointment.Id, appointment.StartDateTime, appointment.EndDateTime, appointment.ConsultantId, appointment.PatientId);
        }
    }
}
