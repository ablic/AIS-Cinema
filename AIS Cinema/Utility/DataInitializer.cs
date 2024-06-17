using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema
{
    public class DataInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Visitor> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var context = new AISCinemaDbContext(serviceProvider.GetRequiredService<DbContextOptions<AISCinemaDbContext>>()))
            {
                context.Database.Migrate();

                if (!context.Roles.Any(r => r.Name == "admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }

                if (!userManager.Users.Any(u => u.UserName == "admin@example.com"))
                {
                    var adminUser = new Visitor { UserName = "admin@example.com", Email = "admin@example.com" };
                    var result = await userManager.CreateAsync(adminUser, "Admin@123456");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "admin");
                    }
                }
            }
        }
    }
}
