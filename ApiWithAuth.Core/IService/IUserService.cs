using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.Utilities;

namespace ApiWithAuth.Core.IService
{
    public interface IUserService
    {
        Task<Response<RegisterDto>> RegisterUserAsync(RegisterDto dto);
        Task<Response<LoginDto>> LoginUserAsync(LoginDto dto);
        Task<Response<LoginDto>> ConfirmEmailAsync(string userId, string token);
    }
}
