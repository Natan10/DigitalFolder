using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

                return CreatedAtAction(nameof(GetTransaction), new { Id = createdTransaction.Id }, createdTransaction);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            try
            {
                var readTransaction = _service.GetTransaction(id);
                if(readTransaction == null) return NotFound();

                return Ok(readTransaction);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                if (result.IsSuccess) return NoContent();
                return NotFound();
            }
            catch 
            {
                return BadRequest();
            }            
        }

    }

}
