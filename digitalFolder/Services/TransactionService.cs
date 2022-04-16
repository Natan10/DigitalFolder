﻿using AutoMapper;
using DigitalFolder.Data;
using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Models;
using DigitalFolder.Models.Enums;
using FluentResults;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFolder.Services
{
  
    public class TransactionService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public TransactionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadTransactionDto> Create(CreateTransactionDto dto)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.Id == dto.WalletId);
            if (wallet == null) return null;

            if (dto.Value < 0) return null;

            if (dto.Type.Equals(TransactionType.Entrada))
            {
                wallet.Balance += dto.Value;
            }
            else
            {
                if ((wallet.Balance - dto.Value) < 0) return null;

                wallet.Balance -= dto.Value;
            }

            var transaction = _mapper.Map<Transaction>(dto);
            
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadTransactionDto>(transaction);
        }

        public ReadTransactionDto GetTransaction(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(transaction => transaction.Id == id);

            if (transaction == null) return null;

            var readTransaction = _mapper.Map<ReadTransactionDto>(transaction);

            return readTransaction;
        }

        public async Task<Result> Delete(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(transaction => transaction.Id == id);

            if (transaction == null) return Result.Fail("Transaction not found");

            if (transaction.Type.Equals(TransactionType.Entrada))
            {
                transaction.Wallet.Balance -= transaction.Value;

            }
            else
            {
                transaction.Wallet.Balance += transaction.Value;
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
