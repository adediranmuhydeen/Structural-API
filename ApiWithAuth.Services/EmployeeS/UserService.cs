using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Core.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWithAuth.Services.EmployeeS
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<Response<RegisterDto>> RegisterUserAsync(RegisterDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("No information is provided");
            if (dto.Password != dto.ConfirmPassword)
            {
                return Response<RegisterDto>.Fail("Password does no match", 400);
            }
            var registerUser = new IdentityUser
            {
                Email = dto.Email,
                UserName = dto.Username,
            };

            var user = await _userManager.CreateAsync(registerUser, dto.Password);
            if (!user.Succeeded)
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(registerUser);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmationToken);
                var validToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}api/auth/confirmEmail?userId={registerUser.Id}&token={validToken}";

                await _mailService.SendEmailAsync(registerUser.Email, "Confrim Your Email Address", "<h2>Welcome to Structural API</h2>" +
                    $"<p>Please confirm your email address by <a href='{url}'>Clicking here</a></p>");


                return Response<RegisterDto>.Fail("User was not created", 400);
            }
            // Todo send confirmation Email
            return Response<RegisterDto>.Success($"User with email {dto.Email} is created successfully", true);
        }

        public async Task<Response<LoginDto>> LoginUserAsync(LoginDto dto)
        {
            //Handling null Dto
            if (dto == null)
            {
                return Response<LoginDto>.Fail("Invalid input", 404);
            }
            //Checking if the Email exist
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return Response<LoginDto>.Fail($"Email {dto.Email} is not registered", 401);
            }
            //Checking if the password supplied matches with the email
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
            {
                return Response<LoginDto>.Fail("Invalid Password", 401);
            }
            //Creating claims
            var claim = new[]
            {
                new Claim("Email", dto.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            //Creating and encoding the Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            //Generating Token
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claim,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return Response<LoginDto>.Success($"{tokenString} Is valid till {token.ValidTo}", true);
        }
        public async Task<Response<LoginDto>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Response<LoginDto>.Fail($"User with Email {email} does not exist ", 404, false);
            }
            var decoded = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decoded);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return Response<LoginDto>.Success("Please complete your registration by confirm the email sent to you", true);
            }
            return Response<LoginDto>.Fail("Operation not successful", 401, false);
        }
    }
}
