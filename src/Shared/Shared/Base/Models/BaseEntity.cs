namespace Shared.Base.Models;

public class BaseEntity
{
    public Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }
}