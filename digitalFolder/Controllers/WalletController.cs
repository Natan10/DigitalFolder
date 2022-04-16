using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
            try
            {
                var userIdClaim = GetCurrentUserId();
                ReadWalletDto createdWallet = _service.Create(dto, userIdClaim);
                if (createdWallet == null) return BadRequest("UserId is incorrect");

                return CreatedAtAction(nameof(GetWallet), new { Id = createdWallet.Id }, createdWallet);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetWallet(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var readWallet = _service.GetWallet(id,userId);
                if(readWallet == null) return NotFound();

                return Ok(readWallet);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetWallets()
        {
            try
            {
                var userId = GetCurrentUserId();
                var wallets = _service.GetAll(userId);
                return Ok(wallets);
            } catch
            {
                return BadRequest();
            }
           
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteWallet(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = _service.Delete(id, userId);
                if(result.IsSuccess) return NoContent();

                return NotFound();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWallet(int id, [FromBody] UpdateWalletDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = _service.Update(id, dto, userId);
                if (result.IsSuccess) return NoContent();

                return NotFound();

            } catch
            {
                return BadRequest();
            }
        }


        private int GetCurrentUserId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value;
            return int.Parse(userId);
        }
    }
}
