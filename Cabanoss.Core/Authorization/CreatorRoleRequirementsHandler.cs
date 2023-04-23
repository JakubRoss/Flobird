using Cabanoss.Core.Common;
using Cabanoss.Core.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Authorization
{
    public class CreatorRoleRequirementsHandler : AuthorizationHandler<CreatorRoleRequirements, Board>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatorRoleRequirements requirement, Board board)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var boardUser = board.BoardUsers.FirstOrDefault(p => p.UserId == userId);
            var boardUserRole = boardUser.Roles;
            if (boardUserRole != Roles.Creator)
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
