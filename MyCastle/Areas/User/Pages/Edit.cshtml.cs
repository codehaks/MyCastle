using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Common;
using MyCastle.Models;

namespace MyCastle.Areas.User.Pages
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId());

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var AppUser = await _userManager.FindByIdAsync(User.GetUserId());

            AppUser.FirstName = FirstName;
            AppUser.LastName = LastName;
            AppUser.Email = Email;

            await _userManager.UpdateAsync(AppUser);
            return RedirectToPage("./index");
        }
    }
}