﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyCastle.Controllers
{
    public class CheckController : Controller
    {
        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            return Ok("Check -> Admin");
        }
    }
}