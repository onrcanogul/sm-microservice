using Shared.Base.Models;

namespace Media.API.Models;

public class Image : BaseEntity
{
    public string Path { get; set; } = null!;
    public string Name { get; set; } = null!;
}