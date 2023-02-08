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
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IMailService mailService, IConfiguration configuration)
        {
            _userService = userService;
            _mailService = mailService;
            _configuration = configuration;
        }

        /// <summary>
        /// Endpont to Add and register uset
        /// </summary>
        /// <remarks>
        ///     Post/Add/Usser:
        ///         {
        ///         "email": "user@example.com",
        ///         "username": "stringstri",
        ///         "password": "string",
        ///         "confirmPassword": "string"
        ///         }
        /// </remarks>
        /// <response code="200">User created</response>
        /// <response code="400">User creation was not successful</response>
        /// <param name="dto"></param>
        /// <returns></returns>


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
            var loginUser = await _userService.LoginUserAsync(login);

            //await _mailService.SendEmailAsync(login.Email, "Login attempt on your account!!!", "<h1>We Noticed a login attempt on your account" +
            //            "</h1><p> This is to inform you that an attempt was made to login to your account on " + DateTime.Now.ToString() + "</p>");
            return Ok(loginUser);
        }

        // /api/auth/confirmEmail?userId&token
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfrimEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(token)) { return NotFound(); }

            var result = await _userService.ConfirmEmailAsync(email, token);
            if (result.Succeded)
            {
                return Redirect($"{_configuration["AppUrl"]}/htmlpage.html");
            }

            return BadRequest(result.Message);
        }


    }



}
