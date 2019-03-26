using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class User : IdentityUser<long>, IEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }        

        [DataType(DataType.Password)]
        public string Password { get; set; }        

        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Post> Posts { get; set; }        
    }
}
