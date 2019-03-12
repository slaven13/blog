using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
