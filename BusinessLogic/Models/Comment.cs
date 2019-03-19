using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public UserInfo UserInfo { get; set; }
        public PostInfo PostInfo { get; set; }
        public long? ParentCommentId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
