using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyCastle.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Index");
        }

        public IActionResult Create()
        {
            return Ok("Create");
        }

        [Authorize(policy: "TehranOnly")]
        public IActionResult Capital()
        {
            return Ok("Create");
        }

        [Authorize(policy: "Adult")]
        public IActionResult Adult()
        {
            return Ok("Adult content");
        }
    }
}