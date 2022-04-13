using DigitalFolder.Data.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public Result SendTokenResetPasswordUser(SendResetRequest resetRequest)
        {
            IdentityUser<int> user = GetIdentityUserByEmail(resetRequest.Email);
            if(user == null) return Result.Fail("User not found");
            
            var token = _signInManager.UserManager.GeneratePasswordResetTokenAsync(user).Result;
            return Result.Ok().WithSuccess(token);

        }

        public Result GetTokenResetPasswordUser(EffectResetRequest effectResetRequest)
        {
            IdentityUser<int> user = GetIdentityUserByEmail(effectResetRequest.Email);
            if (user == null) return Result.Fail("User not found");
            IdentityResult result = _signInManager.UserManager.ResetPasswordAsync(user, effectResetRequest.Token, effectResetRequest.Password).Result;

            if(result.Succeeded) return Result.Ok().WithSuccess("Password Reset Successfully");

            return Result.Fail("Reset Password Fail");
            
        }


        private IdentityUser<int> GetIdentityUserByEmail(string email)
        {
            IdentityUser<int> user = _signInManager.UserManager.Users.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            return user;
        }
    }
}
