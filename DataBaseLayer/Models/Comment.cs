using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
