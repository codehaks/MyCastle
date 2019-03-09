using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCastle.Data;
using MyCastle.Models;
using System.Collections.Generic;

namespace MyCastle.Areas.User.Pages.Bugs
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Bug> Bugs { get; set; }
        public void OnGet([FromServices]AppDbContext db)
        {

            Bugs = db.Bugs;

        }
    }
}