using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Models;
using DigitalFolder.Models.Enums;
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

        public TransactionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == dto.WalletId);
            if (wallet == null) return NotFound();

            if (dto.Value < 0) return BadRequest();
            
            if (dto.Type.Equals(TransactionType.Entrada))
            {
                wallet.Balance += dto.Value;
            }
            else
            {
                if ((wallet.Balance - dto.Value) < 0) return BadRequest("Saldo insuficiente!");

                wallet.Balance -= dto.Value;
            }

            var transaction = _mapper.Map<Transaction>(dto);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetTransaction), new { Id = transaction.Id }, new { Id = transaction.Id });
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(transaction => transaction.Id == id);

            if(transaction == null) return NotFound();

            var readTransaction = _mapper.Map<ReadTransactionDto>(transaction);

            return Ok(readTransaction);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(transaction => transaction.Id == id);

            if (transaction == null) return NotFound();

            if (transaction.Type.Equals(TransactionType.Entrada))
            {
                transaction.Wallet.Balance -= transaction.Value;

            }else
            {
                transaction.Wallet.Balance += transaction.Value;
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return NoContent();

        }

    }

}
