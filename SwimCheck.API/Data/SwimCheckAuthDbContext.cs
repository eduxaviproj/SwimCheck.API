using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwimCheck.API.Data
{
    public class SwimCheckAuthDbContext : IdentityDbContext
    {
        public SwimCheckAuthDbContext(DbContextOptions<SwimCheckAuthDbContext> options) : base(options)
        {

        }

    }
}
