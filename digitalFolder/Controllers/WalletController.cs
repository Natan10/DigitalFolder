using DigitalFolder.Data.Dtos.Pagination;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto dto)
        {
            try
            {
                var userIdClaim = GetCurrentUserId();
                ReadWalletDto createdWallet = await _service.Create(dto, userIdClaim);
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
        public async Task<IActionResult> GetWallets([FromQuery] PaginationRequest @params)
        {
            try
            {
                
                var userId = GetCurrentUserId();
                var wallets = await _service.GetAll(userId,@params.Page,@params.ItemsPerPage);
                //var wallets = await _service.GetAll(userId,1,2);
                return Ok(wallets);
            } catch
            {
                return BadRequest();
            }
           
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.Delete(id, userId);
                if(result.IsSuccess) return NoContent();

                return NotFound();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet(int id, [FromBody] UpdateWalletDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.Update(id, dto, userId);
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
