using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class UserCourseMap : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(a => a.UserCourseId);

            builder.Property(a => a.PurchaseDate).IsRequired();

            builder.HasOne(a => a.User).WithMany(a => a.UserCourses).HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Course).WithMany(a => a.Alunos).HasForeignKey(a => a.CourseId);

            builder.ToTable("Curso Comprado");
        }
    }
}
