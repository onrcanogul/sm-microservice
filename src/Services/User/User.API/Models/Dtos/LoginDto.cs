namespace User.API.Models.Dtos;

public class LoginDto
{
    public string EmailOrUsername { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}