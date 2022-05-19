using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.Authorization
{
    public class CreatedMultipleRestaurantRequirement : IAuthorizationRequirement 
    {
       public CreatedMultipleRestaurantRequirement (int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
        public int MinimumRestaurantsCreated { get;}
    }
}
