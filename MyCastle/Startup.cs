using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyCastle.Common;
using MyCastle.Data;
using MyCastle.Models;

namespace MyCastle
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite("Data Source=app.sqlite"));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
                        ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                        ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                //options.User.RequireUniqueEmail = false;
                //options.Password.RequiredUniqueChars = 0;
                //options.Password.RequiredLength = 6;
                //options.SignIn.RequireConfirmedEmail = false;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(3);  
            });

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.Cookie.Expiration = TimeSpan.FromDays(2);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
                options.AddPolicy("TehranOnly", policy => policy.RequireClaim("City", "Tehran"));
                options.AddPolicy("Adult", policy =>
                {
                    //policy.RequireClaim("City", "Tehran");
                    policy.Requirements.Add(new MinumumAgeRequirement(21));
                });
            });

            //services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("user", "/");
                //options.Conventions.AuthorizeAreaPage("user", "/Edit");
                //options.Conventions.AuthorizeAreaPage("user", "/Profile");

                //options.Conventions.AuthorizeAreaFolder("admin", "/", "RequireAdminRole");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
              name: "default",
              template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
