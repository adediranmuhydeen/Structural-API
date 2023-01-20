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

        /// <summary>
        /// Post method for registering Users
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     {
        ///       "email": "example@example.com",
        ///       "username": "exampleexample",
        ///       "password": "ABCdef123$",
        ///       "confirmPassword": "ABCdef123$"
        ///     }
        /// Sample Response:
        ///     {
        ///       "message": "User with email example@example.com is created successfully",
        ///       "data": null,
        ///       "succeded": true,
        ///       "statusCode": 200
        ///     }
        /// </remarks>


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

        /// <summary>
        /// Post method for generating token that will be used to login  users
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     Post api/Auth/Login/user
        ///     {
        ///         "email": "example@example.com",
        ///         "username": "example",
        ///         "password": "ABCdef123$"
        ///     }
        /// Sample Response(if not successful):
        ///     {
        ///         "message": "Email example@example.com is not registered",
        ///         "data": null,
        ///         "succeded": false,
        ///         "statusCode": 401
        ///     }
        /// Sample Response(if successful):
        ///     {
        ///         "message": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImV4YW1wbGVAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFjZDVkZjRjLWNiNjQtNDA2Mi04OGI4LTk5OTk4MjQ0MGZhMyIsImV4cCI6MTY3NjcwMjU0NywiaXNzIjoiaHR0cDovL2FkZWJheW8ubmV0IiwiYXVkIjoiaHR0cDovL2FkZWJheW8ubmV0In0.u_wdxT1S9FVwLuaBb7yQ0l65raFSeBEaN-McJntdyeI\" Is valid till 18/02/2023 06:42:27",
        ///         "data": null,
        ///         "succeded": true,
        ///         "statusCode": 200
        ///     }
        ///     
        /// </remarks>

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
