using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperations resourceOperations { get; }

        public ResourceOperationRequirement(ResourceOperations resourceOperations) 
        {
            this.resourceOperations = resourceOperations;
        }
    }
}
