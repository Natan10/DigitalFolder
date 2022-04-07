using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Models;
using DigitalFolder.Models.Enums;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private TransactionService _service;

        public TransactionController(AppDbContext context, IMapper mapper, TransactionService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            var createdTransaction = _service.Create(dto);
            if (createdTransaction == null) return BadRequest();

            return CreatedAtAction(nameof(GetTransaction), new { Id = createdTransaction.Id }, createdTransaction);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var readTransaction = _service.GetTransaction(id);
            if(readTransaction == null) return NotFound();

            return Ok(readTransaction);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var result = _service.Delete(id);
            if(result.IsSuccess) return NoContent();

            return NotFound();

        }

    }

}
