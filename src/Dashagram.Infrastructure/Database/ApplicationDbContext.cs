using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Post> Posts { get; set; }

        // Relationships
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
