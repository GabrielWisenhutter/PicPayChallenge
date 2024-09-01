using PicPay.Domain.DTOs.TransferCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Entities
{
    public class Transfer
    {
        public Guid Id { get; private set; }
        public Guid PayerId { get; set; }
        public Wallet? Payer { get; set; }
        public Guid PayeeId { get; set; }
        public Wallet? Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        private Transfer()
        {
        }

        public class Factories
        {
            public static Transfer Create(Guid payerId, Guid payeeId, decimal amount)
            {
                return new Transfer
                {
                    Id = Guid.NewGuid(),
                    PayerId = payerId,
                    PayeeId = payeeId,
                    Amount = amount,
                    Date = DateTime.Now
                };
            }

            public static Transfer Create(NewTransferCommand command)
            {
                return new Transfer
                {
                    Id = Guid.NewGuid(),
                    PayerId = command.PayerId,
                    PayeeId = command.PayeeId,
                    Amount = command.Amount,
                    Date = DateTime.Now
                };
            }
        }
    }
}
