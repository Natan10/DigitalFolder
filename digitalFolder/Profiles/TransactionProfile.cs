using AutoMapper;
using DigitalFolder.Data.Dtos.Transactions;
using DigitalFolder.Models;

namespace DigitalFolder.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<Transaction, ReadTransactionDto>()
                .ForMember(transaction => transaction.Wallet, opts => 
                opts.MapFrom(c =>
                new { Id = c.Wallet.Id , WalletName = c.Wallet.WalletName , Description = c.Wallet.Description, Balance = c.Wallet.Balance  }));
        }
    }
}

