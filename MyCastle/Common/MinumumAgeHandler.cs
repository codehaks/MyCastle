using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCastle.Common
{
    public class MinimumAgeHandler : AuthorizationHandler<MinumumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MinumumAgeRequirement requirement)
        {
            var ageClaim=context.User.FindFirst("Age");
            var age = 1;

            if (ageClaim!=null)
            {
                age = Convert.ToInt32(ageClaim.Value);
            }
            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
