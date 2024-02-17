using Domain.Data.Entities;

namespace Domain.Authentication;

public interface IAuthenticationService
{
    string GenerateJwt(User user, string password);
}