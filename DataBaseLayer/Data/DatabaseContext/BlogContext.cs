using DataBaseAccessLayer.Data.Entities.Mappings;
using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.DatabaseContext
{
    public class BlogContext: DbContext
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
