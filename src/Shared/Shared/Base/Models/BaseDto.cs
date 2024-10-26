namespace Shared.Base.Models;

public class BaseDto
{
    public virtual Guid? Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }
}