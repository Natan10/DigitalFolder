using AutoMapper;
using DigitalFolder.Data.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace DigitalFolder.Services
{
    public class SignInService
    {
        private TokenService _tokenService;
        private SignInManager<IdentityUser<int>> _signInManager;
        public SignInService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result SignInUser(SignInRequest request)
        {
            var resultIdentity = _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (resultIdentity.Result.Succeeded)
            {
                var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == request.Email.ToUpper());
                string generatedToken = _tokenService.CreateToken(user);
                Console.WriteLine(generatedToken);
                return Result.Ok().WithSuccess(generatedToken);
            }

            return Result.Fail("Login fail");

        }
    }
}
