using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperations ResourceOperations { get; }

        public ResourceOperationRequirement(ResourceOperations resourceOperations) 
        {
            ResourceOperations = resourceOperations;
        }
    }
}
