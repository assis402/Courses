using Microsoft.EntityFrameworkCore;
using Courses.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CPF).IsRequired();
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Email).IsRequired();

            builder.Ignore(u => u.Id);
            builder.Ignore(u => u.EmailConfirmed);
            builder.Ignore(u => u.AccessFailedCount);
            builder.Ignore(u => u.LockoutEnabled);
            builder.Ignore(u => u.LockoutEnd);
            builder.Ignore(u => u.PhoneNumberConfirmed);
            builder.Ignore(u => u.TwoFactorEnabled);

            builder.HasMany(u => u.Courses).WithOne(u => u.User).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(u => u.Enderecos).WhitOne(u => u.User);
            builder.HasMany(u => u.Matriculations).WithOne(u => u.User);
            //builder.HasMany(u => u.CoursesProfessor).WithOne(u => u.User);
            builder.HasOne(u => u.Wallet).WithOne(u => u.User);

            builder.ToTable("Users");


        }
    }
}
