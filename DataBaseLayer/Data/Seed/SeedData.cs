using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseAccessLayer.Data.Entities;

namespace DataBaseAccessLayer.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var blogContext = new BlogContext(serviceProvider.GetRequiredService<DbContextOptions<BlogContext>>());
            var userRepository = new Repository<User>(blogContext);
            var postRepository = new Repository<Post>(blogContext);
            var commentRepository = new Repository<Comment>(blogContext);
            
            var first = blogContext.Find(typeof(DataBaseAccessLayer.Data.Entities.User), (long)1 );
            if (first != null)
            {
                return;
            }

            User user1 = new User
            {
                Username = "slaven",
                FirstName = "Slaven",
                SecondName = "Antic",
                Password = "12345678",
                Comments = null,
                Posts = null
            };

            User user2 = new User
            {
                Username = "someone",
                FirstName = "Someone",
                SecondName = "Else",
                Password = "12345678",
                Comments = null,
                Posts = null
            };

            blogContext.AddRange(user1, user2);
            blogContext.SaveChanges();

            var users = userRepository.Get();
            var user1Id = users.Where(u => u.Username == "slaven").FirstOrDefault().Id;
            var user2Id = users.Where(u => u.Username == "someone").FirstOrDefault().Id;

            Post post1 = new Post
            {
                UserId = user1Id,
                User = user1,
                Title = "First Post in the Blog",
                Content = "Content of the Post 1",
                CreationDate = DateTime.UtcNow,
                Comments = null
            };

            Post post2 = new Post
            {
                UserId = user2Id,
                User = user2,
                Title = "Second Post in the Blog",
                Content = "Content of the Post 2",
                CreationDate = DateTime.UtcNow,
                Comments = null
            };

            blogContext.AddRange(post1, post2);
            blogContext.SaveChanges();

            var posts = postRepository.Get();
            var post1Id = posts.Where(p => p.Title.Contains("First Post")).FirstOrDefault().Id;
            var post2Id = posts.Where(p => p.Title.Contains("Second Post")).FirstOrDefault().Id;

            Comment comment1 = new Comment
            {
                UserId = user1Id,
                PostId = post1Id,
                User = user1,
                Post = post1,
                Content = "First Slaven's Comment on Post 1",
                CreationDate = DateTime.UtcNow
            };

            Comment comment2 = new Comment
            {
                UserId = user1Id,
                PostId = post2Id,
                User = user1,
                Post = post2,
                Content = "First Slaven's Comment on Post 2",
                CreationDate = DateTime.UtcNow
            };

            Comment comment3 = new Comment
            {
                UserId = user1Id,
                PostId = post1Id,
                User = user1,
                Post = post1,
                Content = "Second Slaven's Comment on Post 1",
                CreationDate = DateTime.UtcNow
            };

            Comment comment4 = new Comment
            {
                UserId = user2Id,
                PostId = post1Id,
                User = user2,
                Post = post1,
                Content = "First Else's Comment on Post 1",
                CreationDate = DateTime.UtcNow
            };

            Comment comment5 = new Comment
            {
                UserId = user2Id,
                PostId = post2Id,
                User = user2,
                Post = post2,
                Content = "First Else's Comment on Post 2",
                CreationDate = DateTime.UtcNow
            };

            blogContext.AddRange(comment1, comment2, comment3, comment4, comment5);
            blogContext.SaveChanges();
        }
    }
}
