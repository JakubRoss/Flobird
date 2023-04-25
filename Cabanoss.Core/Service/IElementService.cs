using Cabanoss.Core.Model.Element;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IElementService
    {
        Task AddElement(int taskId, ElementDto elementDto, ClaimsPrincipal claims);
        Task CheckElement(int elementId, UpdateElementDto updateElementDto, ClaimsPrincipal claims);
        Task DeleteElement(int elementId, ClaimsPrincipal claims);
        Task<ResponseElementDto> GetElement(int elementId, ClaimsPrincipal claims);
        Task<List<ResponseElementDto>> GetElements(int taskId, ClaimsPrincipal claims);
        Task UpdateElement(int elementId, UpdateElementDto updateElementDto, ClaimsPrincipal claims);
    }
}