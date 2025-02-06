using App.Domain.AppServices.Task.TaskUser;
using App.Domain.AppServices.Task.UserT;
using App.Domain.Core.Entities;
using App.Domain.Core.Task.Configs;
using App.Domain.Core.Task.TaskUser.AppServices;
using App.Domain.Core.Task.TaskUser.Data;
using App.Domain.Core.Task.TaskUser.Services;
using App.Domain.Core.Task.UserT.AppServices;

using App.Domain.Services.Task.TaskUser;
using App.Infra.Data.Db.SqlServer.Ef.Common;
using App.Infra.Data.Repos.Ef.Task.TaskUser;
using Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

var builder = WebApplication.CreateBuilder(args);
#region Configuration

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.AddSingleton(siteSettings);

#endregion
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddIdentity<UserTa, IdentityRole<int>>(option =>
{
    option.SignIn.RequireConfirmedAccount = false;
    option.Password.RequireDigit = false;
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.User.RequireUniqueEmail = true;

})

   .AddRoles<IdentityRole<int>>()
   .AddErrorDescriber<PersianIdentityErrorDescriber>()
   .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/UserT/Login"; // „”?— ’›ÕÂ ·«ê?‰
    options.AccessDeniedPath = "/Account/AccessDenied"; // „”?— ’›ÕÂ ⁄œ„ œ” —”?
});
#region Register Services
builder.Services.AddScoped<IUserTAppServices, UserTAppServices>();

builder.Services.AddScoped<ITaskUserAppServices, TaskUserAppServices>();
builder.Services.AddScoped<ITaskUserServices, TaskUserServices>();
builder.Services.AddScoped<ITaskUserRepository, TaskUserRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(siteSettings.ConnectionStrings.SqlConnection));

#endregion
// Add services to the container.

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
