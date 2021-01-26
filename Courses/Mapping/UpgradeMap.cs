using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class UpgradeMap : IEntityTypeConfiguration<Upgrade>
    {
        public void Configure(EntityTypeBuilder<Upgrade> builder)
        {
            builder.HasKey(u => u.UpgradeId);
            builder.Property(u => u.Title).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CreationDate).IsRequired();

            builder.HasMany(u => u.Features).WithOne(u => u.Upgrade);

            builder.HasOne(u => u.User).WithMany(u => u.Upgrades).HasForeignKey(u => u.UserId);

            builder.ToTable("Upgrades");
        }
    }
}
