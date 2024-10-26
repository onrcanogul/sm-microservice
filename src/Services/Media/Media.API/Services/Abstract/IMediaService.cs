using Media.API.Models;
using Shared.Base;

namespace Media.API.Services.Abstract;

public interface IMediaService<T> where T : Image
{
    Task<ServiceResponse<NoContent>> Upload(IFormFileCollection files, string path, Guid productId);
}