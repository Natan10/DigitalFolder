using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private WalletService _service;

        public WalletController(WalletService service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDto dto)
        {
            ReadWalletDto createdWallet = _service.Create(dto);
            return CreatedAtAction(nameof(GetWallet), new { Id = createdWallet.Id }, createdWallet);
        }

        [HttpGet("{id}")]
        public IActionResult GetWallet(int id)
        {
            var readWallet = _service.GetWallet(id);
            if(readWallet == null) return NotFound();

            return Ok(readWallet);
        }

        [HttpGet]
        public IActionResult GetWallets()
        {
            var wallets = _service.GetAll();  
            return Ok(wallets);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteWallet(int id)
        {
            var result = _service.Delete(id);
            if(result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWallet(int id, [FromBody] UpdateWalletDto dto)
        {
            var result = _service.Update(id, dto);
            if (result.IsSuccess) return NoContent();

            return NotFound();
        }
    }
}
