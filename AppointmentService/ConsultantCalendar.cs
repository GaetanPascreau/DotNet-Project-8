namespace AppointmentService
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConsultantCalendar")]
    public partial class ConsultantCalendar
    {
        public int Id { get; set; }

        public int? ConsultantId { get; set; }

        public DateTime? Date { get; set; }

        public bool? Available { get; set; }
    }
}
