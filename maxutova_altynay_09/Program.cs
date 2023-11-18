using System.Text.Json.Serialization;
using maxutova_altynay_09.Context;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<DefaultContext>(o => o.UseNpgsql(connection))
    .ConfigureApplicationCookie(o =>
    {
        o.AccessDeniedPath = "/Account/Login";
    })
    .AddIdentity<User, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequiredLength = 7;
        opt.Password.RequiredUniqueChars = 2;
    })
    .AddEntityFrameworkStores<DefaultContext>();

builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<UploadFileService>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();