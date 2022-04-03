using AutoMapper;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;
using System.Linq;

namespace DigitalFolder.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<CreateWalletDto, Wallet>();
            CreateMap<Wallet, ReadWalletDto>()
                .ForMember(wallet => wallet.Transactions, 
                opts => opts.MapFrom(wallet => wallet.Transactions.Select(c => 
                new {
                    Id = c.Id ,
                    Type = c.Type,
                    Value = c.Value , 
                    Description = c.Description, 
                    WalletId = c.WalletId,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt

            })));

            CreateMap<UpdateWalletDto, Wallet>().ForAllMembers(opts => opts.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
        }
    }
}
