
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Anime.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var SuperAdminRoleId = "33667c13-6a3f-4d24-830c-a303d0f753f8 ";
            var AdminRoleID = "4dad1a96-c9a8-474a-a077-fbf00e8ce655";
            var UserRoleId = "3758f3ad-8a1c-4af8-9223-030cc5388af6";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin",
                    Id = AdminRoleID,
                    ConcurrencyStamp = AdminRoleID
                },
                new IdentityRole
                {
                    Name ="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id = SuperAdminRoleId,
                    ConcurrencyStamp = SuperAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = UserRoleId,
                    ConcurrencyStamp= UserRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            // Seed super admin user 
            var SuperAdminId = "2d61d1b4-bf7e-4da4-8cf1-df5aa2a78b32";
            var SuperAdminUser = new IdentityUser
            {
                UserName = "SuperAdmin@gmail.com",
                Email = "Superadmin@gmail.com",
                NormalizedEmail = "Superadmin@gmail.com".ToUpper(),
                NormalizedUserName = "superadmin@gmail.com".ToUpper(),
                Id = SuperAdminId
            };
            SuperAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(SuperAdminUser, "superadmin@123");
            builder.Entity<IdentityUser>().HasData(SuperAdminUser);

            // Add all roles to super admin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleID,
                    UserId = SuperAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = SuperAdminRoleId,
                    UserId = SuperAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = UserRoleId,
                    UserId = SuperAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }



    }
}
