using AutoMapper;
using DigitalFolder.Data.Dtos.User;
using DigitalFolder.Models;
using Microsoft.AspNetCore.Identity;

namespace DigitalFolder.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
