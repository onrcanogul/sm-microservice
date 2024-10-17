namespace User.API.Models;

public class Token
{
    public string AccessToken { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}