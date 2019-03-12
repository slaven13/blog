using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class StartPageModel
    {
        public List<Post> MostCommentedPosts { get; set; }
        public List<Post> MostRecentPosts { get; set; }
        public List<Comment> MostRecentCommments { get; set; }
        public List<User> MostActiveUsers { get; set; }
    }
}
