using AutoMapper;
using DigitalFolder.Data.Dtos.User;
using DigitalFolder.Models;

namespace DigitalFolder.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
