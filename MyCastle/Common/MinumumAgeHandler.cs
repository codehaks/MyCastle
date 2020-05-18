using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Common;
using MyCastle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCastle.Common
{
    public class MinimumAgeHandler : AuthorizationHandler<MinumumAgeRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public MinimumAgeHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MinumumAgeRequirement requirement)
        {

            
            var ageClaim=context.User.FindFirst("Age");
            var age = 1;

            if (ageClaim != null)
            {
                age = Convert.ToInt32(ageClaim.Value);
            }
            else
            {
              var user =await _userManager.FindByIdAsync(context.User.GetUserId());
                age = user.Age;
            }
            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            //return Task.CompletedTask;
        }
    }
}
