using AutoMapper;
using DigitalFolder.Data.Dtos.User;
using DigitalFolder.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DigitalFolder.Services
{
    public class SignUpService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;

        public SignUpService(IMapper mapper, UserManager<CustomIdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result SignUpUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            CustomIdentityUser userIdentity = _mapper.Map<CustomIdentityUser>(user);

            userIdentity.UserName = user.Email;
            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity, createUserDto.Password);

            if (resultIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("error when registering user");

        }
    }
}
