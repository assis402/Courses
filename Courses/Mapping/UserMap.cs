using Microsoft.EntityFrameworkCore;
using Courses.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasKey(u => u.UserId);
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CPF).IsRequired();
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.Telefone).IsRequired().HasMaxLength(30);

            builder.Ignore(i => i.EmailConfirmed);
            builder.Ignore(i => i.AccessFailedCount);
            builder.Ignore(i => i.LockoutEnabled);
            builder.Ignore(i => i.LockoutEnd);
            builder.Ignore(i => i.PhoneNumber);
            builder.Ignore(i => i.PhoneNumberConfirmed);
            builder.Ignore(i => i.TwoFactorEnabled);

            //builder.HasMany(u => u.Enderecos).WhitOne(u => u.User);
            builder.HasMany(u => u.UserCourses).WithOne(u => u.User);
            //builder.HasMany(u => u.CoursesProfessor).WithOne(u => u.User);
            builder.HasOne(u => u.Wallet).WithOne(u => u.User);

            builder.ToTable("Alunos");


        }
    }
}
