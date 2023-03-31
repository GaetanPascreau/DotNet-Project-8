using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentService.DTOs
{
    public class ConsultantCalendarDtos
    {
        public record ConsultantCalendarDto(int Id, int ConsultantId, DateTime Date, bool Available);

        public record CreateConsultantCalendarDto([Required][Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                                  [Required] DateTime Date,
                                                  [Required] bool Available = true);

        public record UpdateConsultantCalendarDto([Required][Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                                  [Required] DateTime Date,
                                                  [Required] bool Available = true);
    }
}
