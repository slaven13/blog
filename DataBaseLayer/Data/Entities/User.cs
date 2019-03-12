using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
