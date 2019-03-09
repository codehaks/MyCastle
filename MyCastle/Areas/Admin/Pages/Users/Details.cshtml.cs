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
    public class DetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager)
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
    }
}