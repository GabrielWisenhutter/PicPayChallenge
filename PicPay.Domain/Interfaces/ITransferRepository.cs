using PicPay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Interfaces
{
    public interface ITransferRepository
    {
        public Task Create(Transfer transfer);
    }
}
