using AIS_Cinema;
using AIS_Cinema.Models;
using AIS_Cinema.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AISCinemaDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("AISCinemaDebug")));

builder.Services
    .AddDefaultIdentity<Visitor>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
    })
    .AddEntityFrameworkStores<AISCinemaDbContext>();

builder.Services.AddTransient<ImageWorker>();
builder.Services.AddTransient<TicketEmailSender>();
builder.Services.AddHostedService<SessionCleanupService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Movies}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();