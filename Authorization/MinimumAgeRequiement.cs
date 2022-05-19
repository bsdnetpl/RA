using Microsoft.AspNetCore.Authorization;

namespace RA.Authorization
{
    public class MinimumAgeRequiement: IAuthorizationRequirement
    {
        public int MinimumAge { get;}
        public MinimumAgeRequiement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
        
    }
}
