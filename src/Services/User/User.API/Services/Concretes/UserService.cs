using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Base;
using User.API.Models;
using User.API.Models.Dtos;
using User.API.Services.Abstracts;

namespace User.API.Services.Concretes;

public class UserService(UserManager<Models.User> manager, IMapper mapper, ITokenHandler tokenHandler) : IUserService
{
    public async Task<ServiceResponse<UserDto>> GetById(Guid id)
    {
        var user = await manager.FindByIdAsync(id.ToString());
        var dto = mapper.Map<UserDto>(user);
        return ServiceResponse<UserDto>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<Token>> Login(LoginDto model)
    {
        var user = await CheckUser(model.EmailOrUsername);
        if (user == null) return ServiceResponse<Token>.Failure("No record", StatusCodes.Status400BadRequest);
        var isLoggedIn = await manager.CheckPasswordAsync(user, model.Password);
        if (isLoggedIn == false) return ServiceResponse<Token>.Failure("A problem while login", StatusCodes.Status500InternalServerError);
        var token = tokenHandler.CreateToken(user);
        await UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
        return ServiceResponse<Token>.Success(token,StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> Register(RegisterDto dto)
    {
        var result = await manager.CreateAsync(mapper.Map<Models.User>(dto), dto.Password);
        return result.Succeeded == false ? ServiceResponse<NoContent>.Failure("Failed to create user", StatusCodes.Status400BadRequest) : ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }

    public Task<ServiceResponse<NoContent>> ResetPassword(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<NoContent>> Delete(Guid id)
    {
        var user = await manager.FindByIdAsync(id.ToString()); 
        await manager.DeleteAsync(user);
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
    
    public async Task UpdateRefreshTokenAsync(string? refreshToken, Models.User user, DateTime accessTokenDate, int addToAccessToken)
    {
        // if(user == null) throw new NotFoundException("user not found");
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = accessTokenDate.AddMinutes(addToAccessToken);
        await manager.UpdateAsync(user);
    }
    
    private async Task<Models.User?> CheckUser(string emailOrUsername)
    {
        var user = await manager.FindByEmailAsync(emailOrUsername) ?? await manager.FindByNameAsync(emailOrUsername);
        return user;
    }
}