using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCastle.Common
{
    public class MinumumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; private set; }

        public MinumumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
