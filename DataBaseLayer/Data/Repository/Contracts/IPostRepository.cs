using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository.Contracts
{
    public interface IPostRepository : IRepository<Post>
    {
        IList<Post> GetPostsByUser(long userId);
        IList<Post> GetPostsWithComments();
        Post GetPostWithComments(long postId);
        Post GetPostFull(long postId);
    }
}
