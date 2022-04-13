using DigitalFolder.Data.Requests;
using DigitalFolder.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignInController : ControllerBase
    {
        private SignInService _service;
        public SignInController(SignInService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SignInUser(SignInRequest request)
        {
            Result result = _service.SignInUser(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes[0]);
        }

        [HttpPost("/reset-password")]
        public IActionResult SendTokenResetPasswordUser([FromBody] SendResetRequest resetRequest)
        {
            Result result = _service.SendTokenResetPasswordUser(resetRequest);
            if(result.IsFailed) return Unauthorized(result.Errors);

            return Ok(result.Successes[0]);
        }

        [HttpPost("/reset-token")]
        public IActionResult GetTokenResetPasswordUser(EffectResetRequest effectResetRequest)
        {
            Result result = _service.GetTokenResetPasswordUser(effectResetRequest);
            if (result.IsFailed) return Unauthorized(result.Errors);

            return Ok(result.Successes[0]);
        }


    }
}
