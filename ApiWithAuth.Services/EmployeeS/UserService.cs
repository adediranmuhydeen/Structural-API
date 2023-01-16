using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Core.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<RegisterDto>> RegisterUserAsync(RegisterDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("No information is provided");
            if (dto.Password != dto.ConfirmPassword)
            {
                return Response<RegisterDto>.Fail("Password do no match", 400);
            }
            var registerUser = new IdentityUser
            {
                Email = dto.Email,
                UserName = dto.Username,
            };

            var user = await _userManager.CreateAsync(registerUser, dto.Password);
            if (!user.Succeeded)
            {
                return Response<RegisterDto>.Fail("User was not created", 400);
            }
            // Todo send confirmation Email
            return Response<RegisterDto>.Success($"User with email {dto.Email} is created successfully", dto, true);
        }

        public async Task<Response<LoginDto>> LoginUserAsync(LoginDto dto)
        {
            //Handling null Dto
            if (dto == null)
            {
                throw new ArgumentNullException("Entry cannot be null");
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


            return Response<LoginDto>.Success($"your token is {tokenString}", dto, true);
        }
    }
}
