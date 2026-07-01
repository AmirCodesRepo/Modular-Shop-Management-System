using _0_Framework.Application;
using _0_Framework.Infrastucture;
using AccountManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHost;
using ShopManagement.Configuration;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("shopDb");

services.AddHttpContextAccessor();
DiscountManagementBootstrapper.Configure(services, connectionString);
ShopManagementBootstrapper.Configure(services, connectionString);
InventoryManagementBootstrapper.Configure(services, connectionString);
AccountManagementBootstrapper.Configure(services, connectionString);
services.AddTransient<IFileUploader, FileUploader>();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddSingleton<IPasswordHasher, PasswordHasher>();

services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", builder =>
    builder.RequireRole(new List<string> { MyRoles.Administrator, MyRoles.ContentUploader }));

    options.AddPolicy("DiscountManagement", builder =>
    builder.RequireRole("1"));

});

services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "DiscountManagement");
});

var app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapRazorPages();
app.MapDefaultControllerRoute();


app.Run();