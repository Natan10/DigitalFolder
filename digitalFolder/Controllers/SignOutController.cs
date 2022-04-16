using DigitalFolder.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignOutController : ControllerBase
    {
        private SignOutService _signOutService;
        public SignOutController(SignOutService signOutService)
        {
            _signOutService = signOutService;
        }

        [HttpPost]
        public IActionResult SignOutUser()
        {
            try
            {
                Result result = _signOutService.SignOutUser();
                if(result.IsFailed) return Unauthorized(result.Errors);

                return Ok(result.Successes);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
