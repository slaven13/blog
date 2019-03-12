using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class Blog : BaseEntity
    {
        public IEnumerable<Post> Posts { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
