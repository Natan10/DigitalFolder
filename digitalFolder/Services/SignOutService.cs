using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace DigitalFolder.Services
{
    public class SignOutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public SignOutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result SignOutUser()
        {
            var resultIdentity = _signInManager.SignOutAsync();
            if (resultIdentity.IsCompletedSuccessfully) return Result.Ok();

            return Result.Fail("SignOut fail");
        }
    }
}
