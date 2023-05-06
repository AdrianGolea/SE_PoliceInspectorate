//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using SE_PoliceInspectorate.DataAccess.Abstractions;
//using SE_PoliceInspectorate.DataAccess.EF;
//using SE_PoliceInspectorate.DataAccess.Model;
//using System.Configuration;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();
//builder.Services.AddHttpContextAccessor();

////builder.Services.AddDbContext<PoliceInspectorateContext>(options =>
////    options.UseSqlServer(builder.Configuration.GetConnectionString("PoliceInspectorateContext")));
//builder.Services.AddDbContext<PoliceInspectorateContext>(options =>
//                options.UseSqlServer(
//                    builder.Configuration.GetConnectionString("PoliceInspectorateContext")));
//builder.Services.AddScoped<IUsersRepository, UserRepository>();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

//app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess;
using SE_PoliceInspectorate.DataAccess.Model;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.EF;
using SE_PoliceInspectorate.DataAccess.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>().AddEntityFrameworkStores<PoliceInspectorateContext>();
builder.Services.AddDbContext<PoliceInspectorateContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PoliceInspectorateContext")));

builder.Services.AddScoped<IUsersRepository, UserRepository>();
builder.Services.AddScoped<IConferenceMessageRepository, ConferenceMessageRepository>();
builder.Services.AddScoped<IClassifiedFilesRepository, ClassifiedFileRepository>();




builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User Settings
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConferenceMessage}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ClassifiedFile}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
