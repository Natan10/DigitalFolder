using DigitalFolder.Data.Dtos.User;
using DigitalFolder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SignUpUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var result = await _service.SignUpUser(createUserDto);
                if (result.IsFailed) return BadRequest();
                return Ok();

            } catch
            {
                return BadRequest();
            }
        }
    }
}
