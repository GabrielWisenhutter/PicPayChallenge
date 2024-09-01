using Microsoft.AspNetCore.Mvc;
using PicPay.Domain.DTOs.TransferCommands;
using PicPay.Domain.Handlers;
using PicPay.Domain.Interfaces;

namespace PicPay.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferRepository _repository;
        private readonly TransferHandler _handler;
        public TransferController(ITransferRepository repository, TransferHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewTransferCommand command)
        {
            var result = await _handler.Handle(command);

            return StatusCode(result.Status, result);
        }
    }
}
