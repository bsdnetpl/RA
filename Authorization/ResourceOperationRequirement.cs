using Microsoft.AspNetCore.Authorization;

namespace RA.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOpera)
        {
            ResourceOperation = resourceOpera;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
