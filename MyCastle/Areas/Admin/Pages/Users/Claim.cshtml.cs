using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCastle.Models;

namespace MyCastle.Areas.Admin.Pages.Users
{
    [BindProperties]
    public class ClaimModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser SelectedUser { get; set; }
        public IList<Claim> Claims { get; set; }

        public async Task<IActionResult> OnGet(string userId)
        {
            SelectedUser = await _userManager.FindByIdAsync(userId);
            Claims = await _userManager.GetClaimsAsync(SelectedUser);
            return Page();
        }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByIdAsync(UserId);

            await _userManager.AddClaimAsync(user, new Claim(Type, Value));

            return RedirectToPage("./Index");
        }
    }
}