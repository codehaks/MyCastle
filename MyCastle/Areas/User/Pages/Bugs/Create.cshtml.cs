using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Common;
using MyCastle.Data;
using MyCastle.Models;

namespace MyCastle.Areas.User.Pages.Bugs
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Bug Bug { get; set; }

        private readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnPost()
        {
            Bug.UserId = User.GetUserId();
            _db.Bugs.Add(Bug);
            _db.SaveChanges();

            TempData["message"] = $"New bug created : {Bug.Name}";

            return RedirectToPage("./index");

        }
    }
}