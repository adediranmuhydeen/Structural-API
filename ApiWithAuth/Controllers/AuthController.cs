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

        public AuthController(IUserService userService)
        {
            _userService = userService;
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
                    return Ok(loginUser);
                }
                return BadRequest("Invalid input");
            }
            return BadRequest("Unable to get token");
        }

    }



}
