using Microsoft.AspNetCore.Identity;

namespace User.API.Models;

public class User : IdentityUser<string>
{
    public Guid ProfileImageId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}