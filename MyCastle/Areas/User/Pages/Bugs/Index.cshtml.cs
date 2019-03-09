using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Common;
using MyCastle.Data;
using MyCastle.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCastle.Areas.User.Pages.Bugs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<Bug> Bugs { get; set; }
        public void OnGet([FromServices]AppDbContext db)
        {

            Bugs = db.Bugs.Where(b=>b.UserId==User.GetUserId());

        }
    }
}