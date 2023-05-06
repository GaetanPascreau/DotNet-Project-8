using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CalifornianHealthPortal
{
    [Table("Appointment")]
    public partial class Appointment
    {
        public int Id { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public int? ConsultantId { get; set; }

        public int? PatientId { get; set; }
    }
}
