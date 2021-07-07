using BaseApp.Infra.Data.DbMappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseApp.Infra.Data.Context
{
    public class BaseAppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<IdentityUser> ApplicationUsers { get; set; } = default!;

        public BaseAppDbContext(DbContextOptions options) : base(options)
        {
            System.Console.WriteLine(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerMapping());

            base.OnModelCreating(builder);
        }
    }
}