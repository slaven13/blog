using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class PostPreview
    {
        private string content;

        public long Id { get; set; }
        public string Title { get; set; }

        public string ContentPreview
        {
            get
            {
                return content;
            }
            set
            {
                content = value.Length > Constants.PostPreviewMaxChars ?
                                         ContentOperations.RemoveHtmlTags(content).Substring(0, Constants.PostPreviewMaxChars) + Constants.ContentPreviewAppendix
                                         : value;
            }

        }

        public UserInfo UserInfo { get; set; }

        public List<CommentPreview> CommentsPreview { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}
