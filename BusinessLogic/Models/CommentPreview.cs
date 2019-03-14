using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class CommentPreview
    {
        private string content;

        public long Id { get; set; }
        public string ContentPreview {
            get {
                return content;
            }
            set {
                content = value.Length > Constants.CommentPreviewMaxChars ?
                                           ContentOperations.RemoveHtmlTags(content).Substring(0, Constants.CommentPreviewMaxChars) + Constants.ContentPreviewAppendix
                                           : value;
            }
        }
        public UserInfo UserInfo { get; set; }        
        public PostInfo PostInfo { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
