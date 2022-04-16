using DigitalFolder.Data.Requests;
using DigitalFolder.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SignInUser(SignInRequest request)
        {
            try
            {
                Result result = await _service.SignInUser(request);
                if (result.IsFailed) return Unauthorized(result.Errors);
                return Ok(result.Successes[0]);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("/reset-password")]
        public async Task<IActionResult> SendTokenResetPasswordUser([FromBody] SendResetRequest resetRequest)
        {
            try
            {
                Result result = await _service.SendTokenResetPasswordUser(resetRequest);
                if(result.IsFailed) return Unauthorized(result.Errors);

                return Ok(result.Successes[0]);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("/reset-token")]
        public async Task<IActionResult> GetTokenResetPasswordUser(EffectResetRequest effectResetRequest)
        {
            try
            {
                Result result = await _service.GetTokenResetPasswordUser(effectResetRequest);
                if (result.IsFailed) return Unauthorized(result.Errors);

                return Ok(result.Successes[0]);
            } catch
            {
                return BadRequest();
            }
        }


    }
}
