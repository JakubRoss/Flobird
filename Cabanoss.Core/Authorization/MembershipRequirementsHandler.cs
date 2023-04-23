using Cabanoss.Core.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Authorization
{
    public class MembershipRequirementsHandler : AuthorizationHandler<MembershipRequirements, Board>
    {
        public MembershipRequirementsHandler() 
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MembershipRequirements requirement, Board board)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var boardUser = board.BoardUsers.FirstOrDefault(p => p.UserId == userId);
            if (boardUser == null)
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
