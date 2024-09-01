using PicPay.Domain.Enums;
using PicPay.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.DTOs.WalletCommands
{
    public class NewWalletCommand : Notification, ICommand
    {
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EWalletType Type { get; set; }

        private NewWalletCommand()
        {

        }

        public NewWalletCommand(string fullName, string documentNumber, string email, string password, EWalletType type)
        {
            FullName = fullName;
            DocumentNumber = documentNumber;
            Email = email;
            Password = password;
            Type = type;
        }
        public void Validate()
        {
            ValidateDocumentNumber();
        }

        public void ValidateDocumentNumber()
        {
            if(Type == EWalletType.Common)
                if(DocumentNumber.Length < 11)
                    AddNotification("Document number invalid to Common Wallet");
            if (Type == EWalletType.Merchant)
                if (DocumentNumber.Length < 14)
                    AddNotification("Document number invalid to Merchant Wallet");
        }
    }
}
