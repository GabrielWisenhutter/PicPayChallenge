using Microsoft.AspNetCore.Mvc;
using PicPay.Domain.DTOs.WalletCommands;
using PicPay.Domain.Handlers;
using PicPay.Domain.Interfaces;

namespace PicPay.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WalletControler : ControllerBase
    {
        private readonly IWalletRepository _repository;
        private readonly WalletHandler _handler;
        public WalletControler(IWalletRepository repository, WalletHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewWalletCommand newWallet)
        {
            var result = await _handler.Handle(newWallet);
            return StatusCode(result.Status, result);
        }
    }
}
