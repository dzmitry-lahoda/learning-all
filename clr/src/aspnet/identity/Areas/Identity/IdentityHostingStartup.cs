using System;
using identity.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(identity.Areas.Identity.IdentityHostingStartup))]
namespace identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<identityDb>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("identityDbConnection")));

                services
                    .AddDefaultIdentity<me>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<identityDb>();
                // services
                //     .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                //     .AddRazorPagesOptions(options =>
                //     {
                //         options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                //         options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                //     });             

                // services.ConfigureApplicationCookie(options =>
                // {
                //     options.LoginPath = $"/Identity/Account/Login";
                //     options.LogoutPath = $"/Identity/Account/Logout";
                //     options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                // });                           
            });
        }
    }
}