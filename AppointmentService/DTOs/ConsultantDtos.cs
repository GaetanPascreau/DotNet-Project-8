using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs
{
    public class ConsultantDtos
    {
        public record ConsultantDto(int Id, string FName,string LName,string Speciality);

        public record CreateConsultantDto([Required][StringLength(100)] string FName,
                                          [Required][StringLength(100)] string LName,
                                          [Required][StringLength(50)] string Speciality);



        public record UpdateConsultantDto([Required][StringLength(100)] string FName,
                                           [Required][StringLength(100)] string LName,
                                           [Required][StringLength(50)] string Speciality);
    }
}