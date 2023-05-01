using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.EF;
using SE_PoliceInspectorate.DataAccess.Model;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddDbContext<PoliceInspectorateContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PoliceInspectorateContext")));
builder.Services.AddDbContext<PoliceInspectorateContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("PoliceInspectorateContext")));
builder.Services.AddScoped<IUsersRepository, UserRepository>();


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

app.MapRazorPages();

app.Run();