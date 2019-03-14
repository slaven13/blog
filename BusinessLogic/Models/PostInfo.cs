using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class PostInfo
    {
        public long Id { get; set; }
        public UserInfo UserInfo { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
