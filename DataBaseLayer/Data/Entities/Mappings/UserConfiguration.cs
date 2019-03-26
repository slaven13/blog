using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(u => u.UserName)
                   .HasColumnName("UserName")
                   .IsUnicode(true)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .HasColumnName("Email")
                   .IsUnicode(true)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.EmailConfirmed)
                   .HasColumnName("EmailConfirmed")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(u => u.FirstName)
                   .HasColumnName("FirstName")
                   .IsUnicode(true)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.SecondName)
                   .HasColumnName("SecondName")
                   .IsUnicode(true)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                   .HasColumnName("PasswordHash")
                   .IsUnicode(true)
                   .HasMaxLength(100);

            builder.Property(u => u.SecurityStamp)
                   .HasColumnName("SecurityStamp")
                   .IsUnicode(true)
                   .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .HasColumnName("PhoneNumber")
                   .IsUnicode(true)
                   .HasMaxLength(25);

            builder.Property(u => u.PhoneNumberConfirmed)
                   .HasColumnName("PhoneNumberConfirmed")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(u => u.TwoFactorEnabled)
                   .HasColumnName("TwoFactorEnabled")
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(u => u.LockoutEnabled)
                   .HasColumnName("LockoutEnabled")
                   .HasColumnType("bit")
                   .IsRequired();

            //builder.Property(u => u.LockoutEnd)
            //       .HasColumnName("LockoutEnd")
            //       .HasConversion(new ValueConverter())
            //       .HasColumnType("datetime");

            builder.Property(u => u.AccessFailedCount)
                   .HasColumnName("AccessFailedCount")
                   .HasColumnType("int")
                   .IsRequired();
        }
    }
}
