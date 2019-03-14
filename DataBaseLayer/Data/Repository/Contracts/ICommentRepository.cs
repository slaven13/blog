using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository.Contracts
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IList<Comment> GetCommentsByUser(long userId);
        IList<Comment> GetCommentsByPost(long postId);
        IList<Comment> GetCommentsWithUser();
    }
}
