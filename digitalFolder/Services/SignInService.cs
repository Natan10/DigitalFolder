using DigitalFolder.Data.Requests;
using DigitalFolder.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace DigitalFolder.Services
{
    public class SignInService
    {
        private TokenService _tokenService;
        private SignInManager<CustomIdentityUser> _signInManager;
        public SignInService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
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

        public Result SendTokenResetPasswordUser(SendResetRequest resetRequest)
        {
            CustomIdentityUser user = GetIdentityUserByEmail(resetRequest.Email);
            if(user == null) return Result.Fail("User not found");
            
            var token = _signInManager.UserManager.GeneratePasswordResetTokenAsync(user).Result;
            return Result.Ok().WithSuccess(token);

        }

        public Result GetTokenResetPasswordUser(EffectResetRequest effectResetRequest)
        {
            CustomIdentityUser user = GetIdentityUserByEmail(effectResetRequest.Email);
            if (user == null) return Result.Fail("User not found");
            IdentityResult result = _signInManager.UserManager.ResetPasswordAsync(user, effectResetRequest.Token, effectResetRequest.Password).Result;

            if(result.Succeeded) return Result.Ok().WithSuccess("Password Reset Successfully");

            return Result.Fail("Reset Password Fail");
            
        }


        private CustomIdentityUser GetIdentityUserByEmail(string email)
        {
            CustomIdentityUser user = _signInManager.UserManager.Users.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            return user;
        }
    }
}
