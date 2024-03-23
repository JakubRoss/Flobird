using Domain.Data.Entities;
using System.Security.Claims;

namespace Domain.Authentication;

public interface IAuthenticationService
{
    string GenerateJwt(IEnumerable<Claim>? claims);
}