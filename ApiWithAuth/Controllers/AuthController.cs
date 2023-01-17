using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AuthController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        [HttpPost("/Add/User")]
        public async Task<IActionResult> Create(RegisterDto dto)
        {
            var register = await _userService.RegisterUserAsync(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }
            return Ok(register);
        }

        [HttpPost("Login/User")]

        public async Task<IActionResult> LoginUser(LoginDto login)
        {

            if (ModelState.IsValid)
            {
                var loginUser = await _userService.LoginUserAsync(login);
                if (login != null)
                {
                    await _mailService.SendEmailAsync(login.Email, "Login attempt on your account!!!", "<h1>We Noticed a login attempt on your account" +
                         "</h1><p> This is to inform you that an attempt was made to login to your account on " + DateTime.Now.ToString() + "</p>");
                    return Ok(loginUser);
                }
                return BadRequest("Invalid input");
            }
            return BadRequest("Unable to get token");
        }

    }



}
