using PicPay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Interfaces
{
    public interface IWalletRepository
    {
        public Task<Wallet?> GetById(Guid id);
        public Task<Wallet?> GetByDocumentNumber(string documentNumber);
        public Task<Wallet?> GetByEmail(string email);

        public Task Create(Wallet wallet);
        public Task Update(Wallet wallet);
    }
}
