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
    public class TransferRepository : ITransferRepository
    {
        private readonly PicPayContext _context;
        public TransferRepository(PicPayContext context)
        {
            _context = context;
        }
        public async Task Create(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();
        }
    }
}
