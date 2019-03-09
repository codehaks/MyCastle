using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCastle.Data;
using MyCastle.Models;

namespace MyCastle.Areas.User.Pages.Bugs
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Bug Bug { get; set; }

        public void OnGet(int id)
        {
            Bug = _db.Bugs.Find(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Bugs.Attach(Bug);
            _db.SaveChanges();

            return RedirectToPage("./index");

        }
    }
}