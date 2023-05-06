using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalifornianHealthPortal
{
    [Table("ConsultantCalendar")]
    public partial class ConsultantCalendar
    {
        public int Id { get; set; }

        public int? ConsultantId { get; set; }

        public DateTime? Date { get; set; }

        public bool? Available { get; set; }
    }
}
