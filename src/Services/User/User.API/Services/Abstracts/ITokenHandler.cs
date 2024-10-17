using User.API.Models;

namespace User.API.Services.Abstracts;

public interface ITokenHandler
{
    Token CreateToken(Models.User user);
}