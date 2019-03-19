using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository.Contracts
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IList<Comment> GetComments();
        Comment GetFullComment(long commentId);        
    }
}
