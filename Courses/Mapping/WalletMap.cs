using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Mapping
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.WalletId);
            builder.Property(w => w.Balance).IsRequired();

            builder.HasOne(w => w.User).WithOne(w => w.Wallet).HasForeignKey<Wallet>(w => w.UserId);

            builder.ToTable("Carteira");
        }
    }
}
