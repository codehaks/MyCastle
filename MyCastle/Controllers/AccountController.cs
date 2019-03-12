using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCastle.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCastle.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, IConfiguration configuration,UserManager<ApplicationUser> userManager)
        {
            Configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [Route("/api/token")]
        [HttpPost]
        public async Task<IActionResult> Token(string username,string password)
        {
            var result =await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var token = Generate(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("Not found!");
            }
            
        }

        public IActionResult Index()
        {
            return View();
        }


        public string Generate(ApplicationUser user)
        {
            var secretKey = Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"]); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var claims = _getClaims(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = Configuration["BearerTokens:Issuer"],
                Audience = Configuration["BearerTokens:Audience"],
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private IEnumerable<Claim> _getClaims(ApplicationUser user)
        {
            //JwtRegisteredClaimNames.Sub
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),                
                new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            };

            //var roles = new Role[] { new Role { Name = "Admin" } };
            //foreach (var role in roles)
            //    list.Add(new Claim(ClaimTypes.Role, role.Name));

            return list;
        }
    }
}
