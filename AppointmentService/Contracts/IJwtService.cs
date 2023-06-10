using AppointmentService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppointmentService.Contracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateToken(IdentityUser user);
    }
}
