using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using PicPay.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Infra.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly PicPayContext _context;
        public WalletRepository(PicPayContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetById(Guid id)
        {
            var wallet = await _context.Wallets.Where(x => x.Id == id).FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<Wallet?> GetByDocumentNumber(string documentNumber)
        {
            return await _context.Wallets.Where(x => x.DocumentNumber == documentNumber).FirstOrDefaultAsync();
        }

        public async Task<Wallet?> GetByEmail(string email)
        {
            return await _context.Wallets.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task Create(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
