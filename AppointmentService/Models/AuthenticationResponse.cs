using System;

namespace AppointmentService.Models
{
    public class AuthenticationResponse
    {
        public string token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
