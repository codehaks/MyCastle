using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCastle.Data;
using MyCastle.Models;

namespace MyCastle.Areas.User.Pages.Bugs
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;

        public DetailsModel(AppDbContext db)
        {
            _db = db;
        }

        public Bug Bug { get; set; }

        public void OnGet(int id)
        {
            Bug = _db.Bugs.Find(id);
        }
    }
}