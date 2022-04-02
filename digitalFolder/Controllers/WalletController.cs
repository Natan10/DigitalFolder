using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public WalletController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDto dto)
        {
            Wallet createdWallet = _mapper.Map<Wallet>(dto);
            _context.Wallets.Add(createdWallet);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWallet), new { Id = createdWallet.Id }, createdWallet);
        }

        [HttpGet("{id}")]
        public IActionResult GetWallet(int id)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id);
            if(wallet == null) return NotFound();

            var readWallet = _mapper.Map<ReadWalletDto>(wallet);
            return Ok(readWallet);
        }

    }
}
