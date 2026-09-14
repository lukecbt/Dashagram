using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Dog>()
                .HasOne<AppUser>()
                .WithMany(u => u.Dogs)
                .HasForeignKey(d => d.OwnerId);
        }
    }
}
