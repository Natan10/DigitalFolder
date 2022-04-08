using DigitalFolder.Data.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignUpController: ControllerBase
    {
        public SignUpController()
        {

        }

        [HttpPost]
        public IActionResult SignUpUser(CreateUserDto createUserDto)
        {
            // TODO
            return Ok();
        }
    }
}
