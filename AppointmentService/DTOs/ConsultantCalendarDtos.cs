using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs
{
    public class ConsultantCalendarDtos
    {
        public record ConsultantCalendarDto(int Id, int ConsultantId, DateTime Date, bool Available, byte[] RowVersion);

        public record CreateConsultantCalendarDto([Required][Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                                  [Required] DateTime Date,
                                                  [Required] bool Available = true);

        public record UpdateConsultantCalendarDto([Required][Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                                  [Required] DateTime Date,
                                                  [Required] bool Available = true,
                                                  string RowVersion = null);
    }
}
