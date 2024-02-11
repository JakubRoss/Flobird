using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Service.Impl
{
    public class HttpUserContextService : IHttpUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? UserId => User is null ? null : int.Parse(User.FindFirst(t => t.Type == ClaimTypes.NameIdentifier).Value);
        public string UserLogin => User?.FindFirst(t => t.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }
}
