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
            builder.Property(u => u.Date).IsRequired();

            builder.HasMany(u => u.Features).WithOne(u => u.Upgrade);

            builder.HasOne(a => a.User).WithMany(a => a.Upgrades).HasForeignKey(a => a.UserId);

            builder.ToTable("Upgrades");
        }
    }
}
