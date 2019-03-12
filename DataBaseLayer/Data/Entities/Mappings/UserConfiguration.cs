using DataBaseAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities.Mappings
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> builder)
        {            
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.User)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Posts)
                   .WithOne(p => p.User)
                   .OnDelete(DeleteBehavior.SetNull);

            //builder.HasMany(u => u.Comments);

            //builder.HasMany(u => u.Posts);
        }
    }
}
