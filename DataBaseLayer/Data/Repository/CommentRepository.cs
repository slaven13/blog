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

        public CommentRepository(BlogContext context, IRepository<Comment> commentRepository)
            : base(context)
        {
            this.commentRepository = commentRepository;
        }

        public IList<Comment> GetComments()
        {
            return commentRepository.Get()
                                    .Include(c => c.User)
                                    .Include(c => c.Post)
                                    .Include(c => c.Replies)
                                    .ToList();
        }

        public Comment GetFullComment(long commentId)
        {
            return commentRepository.Get()
                                    .Include(c => c.User)
                                    .Include(c => c.Post)
                                    .Include(c => c.Replies)
                                    .FirstOrDefault(c => c.Id == commentId);
        }
    }
}
