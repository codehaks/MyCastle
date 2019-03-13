using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyCastle.Controllers
{
    public class CheckController : Controller
    {
        [Route("/api/check")]
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Approved 22!");
        }

        [Route("/api/check/admin")]
        [Authorize(Roles ="admin")]
        public IActionResult Admin()
        {
            return Ok("Admin Approved!");
        }


    }
}