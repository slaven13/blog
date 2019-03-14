using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class StartPageModel
    {
        public List<PostPreview> MostCommentedPosts { get; set; }
        public List<PostPreview> MostRecentPosts { get; set; }
        public List<CommentPreview> MostRecentCommments { get; set; }
        public List<UserInfo> MostActiveUsers { get; set; }
    }
}
