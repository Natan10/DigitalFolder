using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ReadWalletDto Create(CreateWalletDto dto)
        {
            Wallet createdWallet = _mapper.Map<Wallet>(dto);
            _context.Wallets.Add(createdWallet);
            _context.SaveChanges();

            ReadWalletDto readWalletDto = _mapper.Map<ReadWalletDto>(createdWallet);

            return readWalletDto;
        }

        public ReadWalletDto GetWallet(int id)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id);
            if (wallet == null) return null;

            var readWallet = _mapper.Map<ReadWalletDto>(wallet);

            return readWallet;
        }

        public Result Delete(int id)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id);
            if (wallet == null) return Result.Fail("Wallet not found"); ;

            _context.Wallets.Remove(wallet);
            _context.SaveChanges();

            return Result.Ok();
        }

        public List<ReadWalletDto> GetAll()
        {
            var wallets = _context.Wallets.ToList();
            var readWallets = _mapper.Map<List<ReadWalletDto>>(wallets);

            return readWallets;
        }

        public Result Update(int id, UpdateWalletDto dto)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id);
            if (wallet == null) return Result.Fail("Wallet not found");

            _mapper.Map(dto, wallet);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
