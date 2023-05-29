using Cabanoss.Core.Model.Element;
using Cabanoss.Core.Model.User;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IElementService
    {
        Task AddElement(int taskId, ElementDto elementDto, ClaimsPrincipal claims);
        Task CheckElement(int elementId, ElementCheckDto updateElementDto, ClaimsPrincipal claims);
        Task DeleteElement(int elementId, ClaimsPrincipal claims);
        Task<ResponseElementDto> GetElement(int elementId, ClaimsPrincipal claims);
        Task<List<ResponseElementDto>> GetElements(int taskId, ClaimsPrincipal claims);
        Task UpdateElement(int elementId, UpdateElementDto updateElementDto, ClaimsPrincipal claims);
        Task<List<ResponseUserDto>> GetElementUsers(int elementId, ClaimsPrincipal claims);
        Task AddUserToElement(int elementId, int userId, ClaimsPrincipal claims);
        Task DeleteUserFromElement(int elementId, int userId, ClaimsPrincipal claims);
    }
}