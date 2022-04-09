using DigitalFolder.Data.Requests;
using DigitalFolder.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

    }
}
