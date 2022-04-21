using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            try
            {
                var createdTransaction = await _service.Create(dto);
                if (createdTransaction == null) return BadRequest();

                return CreatedAtAction(nameof(GetTransaction), new { Id = createdTransaction.Id, WalletId = createdTransaction.WalletId }, createdTransaction);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("/transaction/{id}/wallet/{walletId}")]
        public IActionResult GetTransaction([FromRoute] int id,[FromRoute] int walletId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var readTransaction = _service.GetTransaction(id,walletId, userId);
                if(readTransaction == null) return NotFound();

                return Ok(readTransaction);

            } catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("/transaction/{id}/wallet/{walletId}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id, [FromRoute] int walletId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.Delete(id,walletId,userId);
                if (result.IsSuccess) return NoContent();
                return NotFound();
            }
            catch 
            {
                return BadRequest();
            }            
        }

        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
        }

    }

}
