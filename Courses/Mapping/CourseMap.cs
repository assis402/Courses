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

            builder.Property(c => c.Nome).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Foto).IsRequired();
            builder.Property(c => c.DataCriacao).IsRequired();
            builder.Property(c => c.Preco).IsRequired();

            builder.HasMany(c => c.Alunos).WithOne(c => c.Course).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Cursos");
        }
    }
}
