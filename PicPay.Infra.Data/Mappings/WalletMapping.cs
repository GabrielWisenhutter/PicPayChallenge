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
    public class WalletMapping : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("varchar(36)");
            builder.Property(x => x.FullName).HasColumnName("FullName").HasColumnType("varchar(150)");
            builder.Property(x => x.DocumentNumber).HasColumnName("DocumentNumber").HasColumnType("varchar(14)");
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar(150)");
            builder.Property(x => x.Password).HasColumnName("Password").HasColumnType("varchar(150)");
            builder.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("decimal(18, 2)");
            builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("varchar(10)").HasConversion<string>();


            builder.HasIndex(x => x.DocumentNumber).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
