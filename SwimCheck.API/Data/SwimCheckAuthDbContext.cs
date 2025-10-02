using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwimCheck.API.Data
{
    public class SwimCheckAuthDbContext : IdentityDbContext
    {
        public SwimCheckAuthDbContext(DbContextOptions<SwimCheckAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "d399ce61-a4f4-408e-82a6-6c645f08ea18";
            var writerRoleId = "e44ca178-3839-43df-9961-e34b0f11a106";

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
                }
            };

            builder.Entity<IdentityRole>().HasData(roles); //seeding list of roles to IdentityRole Entity in DB

        }

    }
}
