using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwimCheck.API.Data
{
    //Seeding Identity Roles and Create DataBase
    public class SwimCheckAuthDbContext : IdentityDbContext
    {
        public SwimCheckAuthDbContext(DbContextOptions<SwimCheckAuthDbContext> options) : base(options) //DbContext of type SwimCheckAuthDbContext, so the API knows what DbContext to trigger
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "d399ce61-a4f4-408e-82a6-6c645f08ea18";
            var writerRoleId = "e44ca178-3839-43df-9961-e34b0f11a106";
            var adminRoleId = "223e9e53-2097-4272-b7de-5045d3696bef";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id = readerRoleId,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                },
            };

            builder.Entity<IdentityRole>().HasData(roles); //seeding list of roles to IdentityRole Entity in DB




            // ------------- Seed of admin user + association to Admin role ---------------

            var adminUserId = "3c2fe5d4-1217-49b9-bd96-8ad2106be03e";

            // AspNetUsers
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@swimcheck.local",
                NormalizedUserName = "ADMIN@SWIMCHECK.LOCAL",
                Email = "admin@swimcheck.local",
                NormalizedEmail = "ADMIN@SWIMCHECK.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Password Hash
            var hasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin#12345");

            builder.Entity<IdentityUser>().HasData(adminUser); // Seed admin user to AspNetUsers

            // Assign Admin role to admin user
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUserId,                        // FK for AspNetUsers
                RoleId = adminRoleId                         // FK for AspNetRoles (Admin)
            });

        }

    }
}
