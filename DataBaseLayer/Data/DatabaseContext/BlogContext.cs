using DataBaseAccessLayer.Data.Entities.Mappings;
using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataBaseAccessLayer.Data.DatabaseContext
{
    public class BlogContext: IdentityDbContext<User, IdentityRole<long>, long>
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfiguration(modelBuilder.Entity<User>());
            new PostConfiguration(modelBuilder.Entity<Post>());
            new CommentConfiguration(modelBuilder.Entity<Comment>());            
        }
    }
}
