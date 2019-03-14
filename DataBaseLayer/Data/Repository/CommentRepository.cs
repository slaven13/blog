using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseAccessLayer.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly IRepository<Comment> commentRepository;

        public CommentRepository(BlogContext context,IRepository<Comment> commentRepository)
            : base(context)
        {
            this.commentRepository = commentRepository;
        }

        public IList<Comment> GetCommentsByUser(long userId)
        {
            return commentRepository.FilterBy(c => c.UserId == userId);
        }

        public IList<Comment> GetCommentsByPost(long postId)
        {
            return commentRepository.FilterBy(c => c.PostId == postId);
        }

        public IList<Comment> GetCommentsWithUser()
        {
            return commentRepository.Get().Include(c => c.User).ToList();
        }
    }
}
