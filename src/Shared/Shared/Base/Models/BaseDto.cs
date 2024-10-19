namespace Shared.Base.Models;

public class BaseDto
{
    public Guid? Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}