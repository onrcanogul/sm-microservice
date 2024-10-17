using Shared.Base;
using User.API.Models;
using User.API.Models.Dtos;

namespace User.API.Services.Abstracts;

public interface IUserService
{
    Task<ServiceResponse<UserDto>> GetById(Guid id);
    Task<ServiceResponse<Token>> Login(LoginDto dto);
    Task<ServiceResponse<NoContent>> Register(RegisterDto dto);
    Task<ServiceResponse<NoContent>> ResetPassword(Guid id);
    Task<ServiceResponse<NoContent>> Delete(Guid id);
    Task UpdateRefreshTokenAsync(string refreshToken, Models.User user, DateTime accessTokenDate, int addToAccessToken);
}