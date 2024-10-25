using Shared.Base.Models;

namespace Media.API.Models.Dto;

public class ImageDto : BaseDto
{
    public string Path { get; set; } = null!;
    public string Name { get; set; } = null!;
}