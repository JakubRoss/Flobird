﻿using System.Security.Claims;
using Application.Common;
using Application.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization
{
    public class ResourceOperationRequirementsHandler : AuthorizationHandler<ResourceOperationRequirement, Board>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Board board)
        {
            var userId = int.Parse(context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)!.Value);
            var boardUser = board.BoardUsers.FirstOrDefault(p => p.UserId == userId);

            switch (requirement.ResourceOperations)
            {
                case ResourceOperations.Create:
                    if (boardUser.Roles == Roles.User)
                    {
                        context.Fail();
                        break;
                    }
                    context.Succeed(requirement);
                    break;
                case ResourceOperations.Read:
                    if (boardUser == null)
                    {
                        context.Fail();
                        break;
                    }
                    context.Succeed(requirement);
                    break;
                case ResourceOperations.Update:
                    if (boardUser.Roles ==Roles.User)
                    {
                        context.Fail();
                        break;
                    }
                    context.Succeed(requirement);
                    break;
                case ResourceOperations.Delete:
                    if (boardUser.Roles == Roles.User)
                    {
                        context.Fail();
                        break;
                    }
                    context.Succeed(requirement);
                    break;
                default:
                    context.Fail();
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
