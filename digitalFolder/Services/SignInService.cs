using DigitalFolder.Data.Requests;
using DigitalFolder.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Result> SignInUser(SignInRequest request)
        {
            var resultIdentity = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (resultIdentity.Succeeded)
            {
                var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == request.Email.ToUpper());
                string generatedToken = _tokenService.CreateToken(user);
                return Result.Ok().WithSuccess(generatedToken);
            }

            return Result.Fail("Login fail");

        }

        public async Task<Result> SendTokenResetPasswordUser(SendResetRequest resetRequest)
        {
            CustomIdentityUser user = GetIdentityUserByEmail(resetRequest.Email);
            if(user == null) return Result.Fail("User not found");
            
            var token = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
            return Result.Ok().WithSuccess(token);

        }

        public async Task<Result> GetTokenResetPasswordUser(EffectResetRequest effectResetRequest)
        {
            CustomIdentityUser user = GetIdentityUserByEmail(effectResetRequest.Email);
            if (user == null) return Result.Fail("User not found");
            IdentityResult result = await _signInManager
                .UserManager
                .ResetPasswordAsync(user, effectResetRequest.Token, effectResetRequest.Password);

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
