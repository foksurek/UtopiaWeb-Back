using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using UtopiaWeb.Interfaces;
using Size = System.Drawing.Size;

namespace UtopiaWeb.Services;

public class FileService : IFileService
{
    public async Task SaveFile(string path, string fileName, IFormFile file)
    {
        
        var filePath = Path.Combine(path, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        Console.WriteLine(filePath);
        await file.CopyToAsync(stream);
    }
    public async Task SaveAvatar(string path, string fileName, IFormFile file)
    {
        var filePath = Path.Combine(path, fileName);
        
        await using var stream = file.OpenReadStream();
        using var image = await Image.LoadAsync(stream);

        var resizedImage = ResizeImage(image);
        
        await using var outputStream = File.Create(filePath);
        await resizedImage.SaveAsJpegAsync(outputStream);
    }

    private static Image ResizeImage(Image image)
    {
        image.Mutate(x => x.Resize(256, 256));
        return image;
    }

    public Task DeleteFile(string path, string fileName)
    {
        var extensions = new List<string> {".jpg", ".jpeg", ".png"};
        foreach (var extension in extensions)
        {
            var tempPath = Path.Combine(path, fileName + extension);
            if (File.Exists(tempPath)) File.Delete(tempPath);
        }
        return Task.CompletedTask;
    }
}