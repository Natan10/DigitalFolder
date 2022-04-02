using AutoMapper;
using DigitalFolder.Data.Dtos.Wallet;
using DigitalFolder.Models;

namespace DigitalFolder.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<CreateWalletDto, Wallet>();
            CreateMap<Wallet, ReadWalletDto>();
            CreateMap<UpdateWalletDto, Wallet>().ForAllMembers(opts => opts.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
        }
    }
}
