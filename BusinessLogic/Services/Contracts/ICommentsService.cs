using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface ICommentsService
    {
        List<Comment> GetPostComments(long postId);
        Comment GetComment(long commentId);
        void AddComment(Comment post);
        void DeleteComment(long postId);
        void EditComment(Comment comment);
    }
}
