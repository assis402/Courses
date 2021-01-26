using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Image).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
            builder.Property(c => c.Price).IsRequired();

            builder.HasOne(c => c.User).WithMany(c => c.Courses).HasForeignKey(c => c.UserId);
            builder.HasMany(c => c.Matriculations).WithOne(c => c.Course).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Courses");
        }
    }
}
