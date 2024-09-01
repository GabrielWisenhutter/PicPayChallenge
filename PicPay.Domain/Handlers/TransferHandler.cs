using PicPay.Domain.DTOs;
using PicPay.Domain.DTOs.TransferCommands;
using PicPay.Domain.Entities;
using PicPay.Domain.Enums;
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
        public TransferHandler(ITransferRepository transferRepository, IWalletRepository walletRepository)
        {
            _transferRepository = transferRepository;
            _walletRepository = walletRepository;
        }
        public async Task<BaseResult> Handle(NewTransferCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new Result<List<string>>(400, "Um ou mais erros", command.Messages);
            
            var validateResult = await ValidateTransfer(command.PayerId, command.PayeeId, command);
            if (validateResult.Status != 200)
                return new Result<string>(validateResult.Status, validateResult.Message ?? string.Empty);

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

        private async Task<Result<string>> ValidateTransfer(Guid payerId, Guid payeeId, NewTransferCommand command)
        {
            var payer = await _walletRepository.GetById(payerId);
            var payee = await _walletRepository.GetById(payeeId);

            if (payer == null || payee == null)
                return new Result<string>(404, "Payer or Payee not found");

            if (payer.Type == EWalletType.Merchant)
                return new Result<string>(400, "Merchant can't do transactions");

            if (payer.Amount < command.Amount)
                return new Result<string>(400, "Payer amount is smaller than the value of transaction");

            return new Result<string>(200, "Transfer is valid");
        }
    }
}
