using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public UserInfo UserInfo { get; set; }        
        public List<Comment> Comments { get; set; }
        public DateTime CreationDate { get; set; }       
    }
}
