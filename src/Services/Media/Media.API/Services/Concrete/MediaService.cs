using Media.API.Contexts;
using Media.API.Models;
using Media.API.Services.Abstract;
using Shared.Base;
using Shared.Base.Repository;
using Shared.Base.UnitOfWork;

namespace Media.API.Services.Concrete;

public class MediaService<T>(IRepository<T, ImageDbContext> repository, IUnitOfWork<ImageDbContext> unitOfWork, IWebHostEnvironment webHostEnvironment)
    :IMediaService<T> where T : Image
{
    public async Task<ServiceResponse<NoContent>> DeleteAsync(Guid id, string fileName, string path)
    { 
        File.Delete($"{path}\\{fileName}");
        await DeleteImageAsync(id);
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,
                useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
    }
    
    private async Task DeleteImageAsync(Guid imageId)
    {
        repository.Delete(await repository.GetFirstOrDefaultAsync(x => x.Id == imageId));
        await unitOfWork.CommitAsync();
    }
    private async Task<string> FileRenameAsync(string path, string fileName, int num = 0)
    {
        var newFileName = await Task.Run<string>(async () =>
        {
            string newFileName;
            var extension = Path.GetExtension(fileName);
            if (num == 0)
                newFileName = NameOperation.CharacterRegulatory(Path.GetFileNameWithoutExtension(fileName)) + extension;
            else
                newFileName = fileName;
            if (File.Exists($"{path}\\{newFileName}"))
                return await FileRenameAsync(path, $"{Path.GetFileNameWithoutExtension(newFileName)}-{num}{extension}",
                    ++num);
            return newFileName;
        });
        return newFileName;
    }

    public async Task<ServiceResponse<NoContent>> Upload(IFormFileCollection files, string path, Guid productId)
    {
        var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        List<(string fileName, string path)> data = new();
        List<bool> results = new();
        foreach (var file in files)
        {
            var fileNewName = await FileRenameAsync(uploadPath, file.Name);
            var result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
            results.Add(result);
            data.Add((fileNewName, $"{path}\\{fileNewName}"));
        }
        if (!results.TrueForAll(r => r.Equals(true)))
            throw new Exception();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}