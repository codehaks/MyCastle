using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCastle.Data;
using MyCastle.Models;

namespace MyCastle.Areas.User.Pages.Bugs
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;

        public DeleteModel(AppDbContext db)
        {
            _db = db;
        }

        
        public Bug Bug { get; set; }

        public void OnGet(int id)
        {
            Bug = _db.Bugs.Find(id);
        }

        [BindProperty]
        public int Id { get; set; }

        public IActionResult OnPost()
        {
            var bug = _db.Bugs.Find(Id);

            _db.Bugs.Remove(bug);
            _db.SaveChanges();

            return RedirectToPage("./index");

        }
    }
}