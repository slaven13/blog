using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public virtual Comment ParentComment { get; set; }
        public DateTime CreationDate { get; set; }
        public long? ParentCommentId { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long PostId { get; set; }
        public virtual Post Post { get; set; }

        
    }
}
