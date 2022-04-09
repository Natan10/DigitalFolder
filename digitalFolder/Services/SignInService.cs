using AutoMapper;
using DigitalFolder.Data.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace DigitalFolder.Services
{
    public class SignInService
    {
        private IMapper _mapper;
        private SignInManager<IdentityUser<int>> _signInManager;
        public SignInService(IMapper mapper, SignInManager<IdentityUser<int>> signInManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public Result SignInUser(SignInRequest request)
        {
            var resultIdentity = _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (resultIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("Login fail");

        }
    }
}
