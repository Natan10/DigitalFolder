using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFolder.Services
{
    public class WalletService
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public WalletService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ReadWalletDto> Create(CreateWalletDto dto, int userId)
        {

            if(dto.UserId != userId) return null;

            Wallet createdWallet = _mapper.Map<Wallet>(dto);
            await _context.Wallets.AddAsync(createdWallet);
            await _context.SaveChangesAsync();

            ReadWalletDto readWalletDto = _mapper.Map<ReadWalletDto>(createdWallet);

            return readWalletDto;
        }

        public ReadWalletDto GetWallet(int id, int userId)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id && wallet.UserId == userId);
            if (wallet == null) return null;

            var readWallet = _mapper.Map<ReadWalletDto>(wallet);

            return readWallet;
        }

        public async Task<Result> Delete(int id, int userId)
        {

            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id && wallet.UserId == userId);
            if (wallet == null) return Result.Fail("Wallet not found"); ;

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public List<ReadWalletDto> GetAll(int userId)
        {

            var wallets = _context.Wallets.Where(w => w.UserId == userId).ToList();
            var readWallets = _mapper.Map<List<ReadWalletDto>>(wallets);

            return readWallets;
        }

        public async Task<Result> Update(int id, UpdateWalletDto dto, int userId)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id && wallet.UserId == userId);
            if (wallet == null) return Result.Fail("Wallet not found");

            _mapper.Map(dto, wallet);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
