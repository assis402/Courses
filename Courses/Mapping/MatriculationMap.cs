using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class MatriculationMap : IEntityTypeConfiguration<Matriculation>
    {
        public void Configure(EntityTypeBuilder<Matriculation> builder)
        {
            builder.HasKey(a => a.MatriculationId);

            builder.Property(a => a.CreationDate).IsRequired();

            builder.HasOne(a => a.User).WithMany(a => a.Matriculations).HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Course).WithMany(a => a.Matriculations).HasForeignKey(a => a.CourseId);

            builder.ToTable("Matriculations");
        }
    }
}
