using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace AIS_Cinema
{
    public class AISCinemaDbContext : IdentityDbContext<Visitor>
    {
        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Session> Sessions { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<AgeLimit> AgeLimits { get; set; } = default!;
        public DbSet<Hall> Halls { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<Visitor> Visistors { get; set; } = default!;

        public AISCinemaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .HasMany(m => m.Sessions)
                .WithOne(s => s.Movie)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AgeLimit>().HasData(
                new AgeLimit
                {
                    Id = 1,
                    Value = "0+"
                }, 
                new AgeLimit
                {
                    Id = 2,
                    Value = "6+"
                },
                new AgeLimit
                {
                    Id = 3,
                    Value = "12+"
                },
                new AgeLimit
                {
                    Id = 4,
                    Value = "16+"
                },
                new AgeLimit
                {
                    Id = 5,
                    Value = "18+"
                });

            builder.Entity<Movie>().Property(m => m.AgeLimitId).HasDefaultValue(1);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "6b7bf0ac-b815-455a-8908-8133983c9200",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<Visitor>().HasData(new Visitor
            {
                Id = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                UserName = "Администратор",
                NormalizedUserName = "АДМИНИСТРАТОР",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "root"),
                SecurityStamp = string.Empty,
                ConcurrencyStamp = string.Empty,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "6b7bf0ac-b815-455a-8908-8133983c9200",
                UserId = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7"
            });

            builder.Entity<Hall>().HasData(
            new Hall
            {
                Id = 1,
                Schema = JsonConvert.SerializeObject(HallTemplates.Simple5x5),
            },
            new Hall
            {
                Id = 2,
                Schema = JsonConvert.SerializeObject(HallTemplates.Complex8),
            },
            new Hall
            {
                Id = 3,
                Schema = JsonConvert.SerializeObject(HallTemplates.Triangle),
            });
        }
    }
}