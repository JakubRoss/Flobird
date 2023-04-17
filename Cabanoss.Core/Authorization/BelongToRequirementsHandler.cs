using Cabanoss.Core.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Authorization
{
    public class BelongToRequirementsHandler : AuthorizationHandler<BelongToRequirements, Board>
    {
        public BelongToRequirementsHandler() 
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BelongToRequirements requirement, Board board)
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
