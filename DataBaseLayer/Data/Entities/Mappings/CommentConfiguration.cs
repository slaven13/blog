using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities.Mappings
{
    public class CommentConfiguration
    {
        public CommentConfiguration(EntityTypeBuilder<Comment> builder)
        {            
            builder.ToTable("Comments", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ParentComment)
                   .WithOne()
                   .HasForeignKey("Comment", "ParentCommentId")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
