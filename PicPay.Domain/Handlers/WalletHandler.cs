using PicPay.Domain.DTOs.WalletCommands;
using PicPay.Domain.DTOs;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicPay.Domain.Extensions;

namespace PicPay.Domain.Handlers
{
    public class WalletHandler : IHandler<NewWalletCommand>
    {
        private readonly IWalletRepository _repository;
        private readonly ISecurityService _securityService;
        private readonly Notification _notification;
        public WalletHandler(IWalletRepository repository, ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
            _notification = new Notification();
        }
        public async Task<BaseResult> Handle(NewWalletCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new Result<List<string>>(400, "Please, check the errors in 'Data' object", command.Messages);

            await ValidateBusinessRules(command);
            if (!_notification.IsValid)
                return new Result<List<string>>(400, "One or more errors", _notification.Messages);


            var hashPasssword = _securityService.GenerateHash(command.Password);
            command.Password = hashPasssword;
            var wallet = Wallet.Factories.Create(command);
            await _repository.Create(Wallet.Factories.Create(command));
            return new Result<Wallet>(201, "Wallete created", wallet);
        }

        #region Validation methods
        private async Task ValidateBusinessRules(NewWalletCommand command)
        {
            await CheckIfDocumentNumberExist(command);
            await CheckIfEmailExists(command);
        }
        private async Task CheckIfDocumentNumberExist(NewWalletCommand command)
        {
            var wallet = await _repository.GetByDocumentNumber(command.DocumentNumber);

            if (wallet != null)
                _notification.AddNotification("An account with this document number already exists");
        }
        public async Task CheckIfEmailExists(NewWalletCommand command)
        {
            var wallet = await _repository.GetByEmail(command.Email);

            if (wallet != null)
                _notification.AddNotification("An account with that Email already exists");
        }
        #endregion
    }
}
