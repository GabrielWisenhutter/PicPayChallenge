using PicPay.Domain.DTOs.WalletCommands;
using PicPay.Domain.DTOs;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Handlers
{
    public class WalletHandler : IHandler<NewWalletCommand>
    {
        private readonly IWalletRepository _repository;
        private readonly ISecurityService _securityService;
        public WalletHandler(IWalletRepository repository, ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
        }
        public async Task<BaseResult> Handle(NewWalletCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new Result<List<string>>(400, "Por favor verifique os campos com erro no objeto { - Data - }", command.Messages);

            if (await CheckIfEmailExists(command))
                return new Result<string>(400, "Email exists.");
            if (await CheckIfDocumentNumberExist(command))
                return new Result<string>(400, "DocumentNumber exists.");


            var hashPasssword = _securityService.GenerateHash(command.Password);
            command.Password = hashPasssword;
            var wallet = Wallet.Factories.Create(command);
            await _repository.Create(wallet);
            return new Result<Wallet>(201, "Wallete created", wallet);
        }

        private async Task<bool> CheckIfDocumentNumberExist(NewWalletCommand command)
        {
            var wallet = await _repository.GetByDocumentNumber(command.DocumentNumber);

            if (wallet != null)
                return true;
            return false;
        }

        public async Task<bool> CheckIfEmailExists(NewWalletCommand command)
        {
            var wallet = await _repository.GetByEmail(command.Email);

            if (wallet != null)
                return true;
            return false;
        }
    }
}
