using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
    }
}
