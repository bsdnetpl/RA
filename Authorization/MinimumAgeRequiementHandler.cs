using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace RA.Authorization
{
    public class MinimumAgeRequiementHandler : AuthorizationHandler<MinimumAgeRequiement>
    {
        private readonly ILogger<MinimumAgeRequiementHandler> _logger;

        public MinimumAgeRequiementHandler(ILogger<MinimumAgeRequiementHandler> logger)
        {
           _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequiement requirement)
        {
          var dataOfBirth =  DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
          
          var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User:{userEmail} with date of birth: [{dataOfBirth}]"); 

            if(dataOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Athorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Athorization failed");
            }
          return Task.CompletedTask;
        }
    }
}
