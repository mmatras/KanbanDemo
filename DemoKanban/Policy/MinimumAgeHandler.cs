using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DemoKanban.Policy
{
    public class MinimumAgeHandler : AuthorizationHandler<IsOverRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOverRequirement requirement)
        {
            var dateOfBirth = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth)?.Value;

            if(dateOfBirth != null) {

                var age = DateTime.Now.Year - DateTime.Parse(dateOfBirth).Year;
                
                if(requirement.Age < age)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
