using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using PicPay.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Infra.Data.Context
{
    public class PicPayContext : DbContext
    {
        public PicPayContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WalletMapping());
            modelBuilder.ApplyConfiguration(new TransferMapping());
        }
    }
}
