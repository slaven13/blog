using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IPostsService
    {
        StartPageModel GetStartPage();
        Post GetPost(long postId);
        void AddPost(Post post);
        void DeletePost(long postId);
        void EditPost(Post post);
    }
}
