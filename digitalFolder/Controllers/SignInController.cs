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
           Result result =  _service.SignInUser(request);
            if(result.IsFailed) return Unauthorized();

            return Ok();
        }

    }
}
