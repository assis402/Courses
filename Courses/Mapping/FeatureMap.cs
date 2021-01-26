using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class FeatureMap : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(u => u.FeatureId);
            builder.Property(u => u.Title).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CreationDate).IsRequired();

            builder.HasOne(a => a.User).WithMany(a => a.Features).HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Upgrade).WithMany(a => a.Features).HasForeignKey(a => a.UpgradeId);

            builder.ToTable("Features");
        }
    }
}
