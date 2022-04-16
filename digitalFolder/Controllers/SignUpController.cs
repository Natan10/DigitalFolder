using DigitalFolder.Data.Dtos.User;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignUpController: ControllerBase
    {
        private SignUpService _service;

        public SignUpController(SignUpService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SignUpUser([FromBody] CreateUserDto createUserDto)
        {
            var result = _service.SignUpUser(createUserDto);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }
    }
}
