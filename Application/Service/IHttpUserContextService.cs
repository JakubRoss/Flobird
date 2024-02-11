using System.Security.Claims;

namespace Application.Service
{
    public interface IHttpUserContextService
    {
        ClaimsPrincipal User { get; }
        int? UserId { get; }
        string UserLogin { get; }
    }
}