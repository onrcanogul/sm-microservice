namespace Media.API.Models;

public class UserImage : Image
{
    public Guid UserId { get; set; }
}