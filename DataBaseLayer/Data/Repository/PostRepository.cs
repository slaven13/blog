﻿using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseAccessLayer.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly IRepository<Post> postRepository;

        public PostRepository(BlogContext context, IRepository<Post> postRepository)
            : base(context)
        {
            this.postRepository = postRepository;
        }

        public Post GetPostFull(long postId)
        {
            return postRepository.Get()
                                 .Include(p => p.Comments)
                                 .Include(p => p.User)
                                 .FirstOrDefault(p => p.Id == postId);
        }          

        public IList<Post> GetPosts()
        {
            return postRepository.Get()
                                 .Include(p => p.Comments)
                                 .Include(p => p.User)
                                 .ToList();
        }        
    }
}
