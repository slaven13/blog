using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class Post : BaseEntity
    {        
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
