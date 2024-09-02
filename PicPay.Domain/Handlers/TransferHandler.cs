using PicPay.Domain.DTOs;
using PicPay.Domain.DTOs.TransferCommands;
using PicPay.Domain.Entities;
using PicPay.Domain.Enums;
using PicPay.Domain.Extensions;
using PicPay.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Handlers
{
    public class TransferHandler : IHandler<NewTransferCommand>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly Notification _notification;
        public TransferHandler(ITransferRepository transferRepository, IWalletRepository walletRepository, Notification notification)
        {
            _transferRepository = transferRepository;
            _walletRepository = walletRepository;
            _notification = notification;
        }
        public async Task<BaseResult> Handle(NewTransferCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new Result<List<string>>(400, "Um ou mais erros", command.Messages);
            
            await ValidateTransfer(command.PayerId, command.PayeeId, command);
            if (!_notification.IsValid)
                return new Result<List<string>>(400, "One or more errors", _notification.Messages);


            await TransferAmount(command.PayerId, command.PayeeId, command);
            await _transferRepository.Create(Transfer.Factories.Create(command));

            return new Result<string>(200, "Transfer completed");
        }

        private async Task TransferAmount(Guid payerId, Guid payeeId, NewTransferCommand command)
        {
            var payer = await _walletRepository.GetById(payerId);
            var payee = await _walletRepository.GetById(payeeId);

            payer!.Amount -= command.Amount;
            payee!.Amount += command.Amount;

            await _walletRepository.Update(payee);
            await _walletRepository.Update(payer);
        }

        private async Task ValidateTransfer(Guid payerId, Guid payeeId, NewTransferCommand command)
        {
            var payer = await _walletRepository.GetById(payerId);
            var payee = await _walletRepository.GetById(payeeId);

            if (payer == null || payee == null)
            {
                _notification.AddNotification("Payer or Payee not found");
                return;
            }
             
            if (payer.Type == EWalletType.Merchant)
                _notification.AddNotification("Merchant can't do transactions");

            if (payer.Amount < command.Amount)
                _notification.AddNotification("Payer amount is smaller than the value of transaction");
        }
    }
}
