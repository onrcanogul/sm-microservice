using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using User.API.Models;
using User.API.Services.Abstracts;

namespace User.API.Services.Concretes;

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    public Token CreateToken(Models.User user)
    {
        Token token = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!));
        SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);
        token.Expiration = DateTime.UtcNow.AddMinutes(15);


        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserName", user.UserName),
            new Claim("Id", user.Id)
        };
            
        JwtSecurityToken jwtSecurityToken = new(
            issuer: configuration["Token:Issuer"],
            audience: configuration["Token:Audience"],
            notBefore: DateTime.UtcNow,
            expires: token.Expiration,
            signingCredentials: signingCredentials,
            claims: claims
        );
        JwtSecurityTokenHandler handler = new();
        token.AccessToken = handler.WriteToken(jwtSecurityToken);
        token.RefreshToken = CreateRefreshToken();
        return token;
    }
    private static string CreateRefreshToken()
    {
        var number = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}