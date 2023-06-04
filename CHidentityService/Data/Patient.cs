using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CHidentityService.Data
{
    public class Patient : IdentityUser
    {
        [StringLength(50)]
        public string FName { get; set; }

        [StringLength(50)]
        public string LName { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }
    }
}
