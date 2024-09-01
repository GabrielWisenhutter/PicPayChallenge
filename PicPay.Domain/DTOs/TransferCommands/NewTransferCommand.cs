using PicPay.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.DTOs.TransferCommands
{
    public class NewTransferCommand : Notification, ICommand
    {
        public Guid PayerId { get; set; }
        public Guid PayeeId { get; set; }
        public decimal Amount { get; set; }

        public void Validate()
        {
            if (PayerId.ToString().Length < 36)
                AddNotification("Payer Id inválido");
            if (PayeeId.ToString().Length < 36)
                AddNotification("Payee Id inválido");
        }
    }
}
