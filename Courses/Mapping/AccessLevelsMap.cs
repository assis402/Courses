using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class AccessLevelsMap : IEntityTypeConfiguration<AccessLevels>
    {
        public void Configure(EntityTypeBuilder<AccessLevels> builder)
        {
            builder.Property(n => n.Description).IsRequired().HasMaxLength(400);

            builder.ToTable("AccessLevels");

        }
    }
}
