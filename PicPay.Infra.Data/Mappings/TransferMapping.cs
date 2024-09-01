using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Infra.Data.Mappings
{
    public class TransferMapping : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("varchar(36)").IsRequired();
            builder.HasOne(x => x.Payer).WithMany(x => x.PaidTransfers).HasForeignKey(x => x.PayerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Payee).WithMany(x => x.ReceivedTransfers).HasForeignKey(x => x.PayeeId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Date).HasColumnName("Date").HasColumnType("datetime").IsRequired().HasDefaultValue(DateTime.MinValue);
        }
    }
}
