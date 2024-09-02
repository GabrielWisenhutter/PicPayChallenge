using PicPay.Domain.DTOs.WalletCommands;
using PicPay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; private set; }
        public EWalletType Type { get; set; }
        public ICollection<Transfer> PaidTransfers { get; set; } = new List<Transfer>();
        public ICollection<Transfer>? ReceivedTransfers { get; set; } = new List<Transfer>();

        private Wallet()
        {   
        }

        public void IncreaseBalance(decimal value)
        {
            Amount += value;
        }

        public bool DecreaseBalance(decimal value)
        {
            if (Amount >= value)
            {
                Amount -= value;
                return true;
            }
            return false;
        }

        public class Factories
        {
            public static Wallet Create(string fullName, string documentNumber, string email, string password, EWalletType type)
            {
                return new Wallet
                {
                    Id = Guid.NewGuid(),
                    FullName = fullName,
                    DocumentNumber = documentNumber,
                    Email = email,
                    Password = password,
                    Type = type,
                    Amount = 0
                };
            }

            public static Wallet Create(NewWalletCommand command)
            {
                return new Wallet
                {
                    Id = Guid.NewGuid(),
                    FullName = command.FullName,
                    DocumentNumber = command.DocumentNumber,
                    Email = command.Email,
                    Password = command.Password,
                    Type = command.Type,
                    Amount = 0
                };
            }
        }
    }
}
