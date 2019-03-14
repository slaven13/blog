using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class CommentInfo
    {
        private string content;

        public long Id { get; set; }
        public string ContentPreview {
            get {
                return content;
            }
            set {
                content = content.Length > Constants.CommentPreviewMaxChars ?
                                           ContentOperations.RemoveHtmlTags(content).Substring(0, Constants.CommentPreviewMaxChars) + Constants.ContentPreviewAppendix
                                           : content;
            }
        }
        public UserInfo UserInfo { get; set; }
    }
}
