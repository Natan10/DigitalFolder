using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos;
using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PaginationResponse<ReadWalletDto>> GetAll(int userId,int page, int itemsPerPage)
        {
            page = page < 1 ? 1 : page;
            itemsPerPage = itemsPerPage < 1 ? 1 : itemsPerPage;

            var wallets =  _context.Wallets
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.CreatedAt);

            var pagination = new PaginationResponse<ReadWalletDto>(wallets.Count(),page,itemsPerPage);

            var dataWallet = await wallets
                .Skip((pagination.Page - 1) * pagination.ItemsPerPage)
                .Take(pagination.ItemsPerPage)
                .ToListAsync();
            
            var readWallets = _mapper.Map<List<ReadWalletDto>>(dataWallet);

            pagination.Data = readWallets;

            return pagination;
        }

        public PaginationResponse<ReadTransactionDto> GetTransactions(int userId, int walletId, int page, int itemsPerPage)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.UserId == userId && wallet.Id == walletId);

            if(wallet == null) return null;

            var transactions = wallet.Transactions.OrderByDescending(t => t.CreatedAt);

            var pagination = new PaginationResponse<ReadTransactionDto>(transactions.Count(), page, itemsPerPage);

            var dataTransactions = transactions
               .Skip((pagination.Page - 1) * pagination.ItemsPerPage)
               .Take(pagination.ItemsPerPage)
               .ToList();

            var readTransactions = _mapper.Map<List<ReadTransactionDto>>(dataTransactions);

            pagination.Data = readTransactions;

            return pagination;

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
