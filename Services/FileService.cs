using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using UtopiaWeb.Interfaces;
using Size = System.Drawing.Size;

namespace UtopiaWeb.Services;

public class FileService : IFileService
{
    public async Task SaveFile(string path, string fileName, Image image, string type)
    {
        var resizedImage = type switch
        {
            "avatar" => ResizeImage(image),
            "banner" => ResizeImage(image, 1600, 900),
            _ => image
        };
        await using var outputStream = File.Create(Path.Combine(path, fileName));
        await resizedImage.SaveAsJpegAsync(outputStream);
    }

    private static Image ResizeImage(Image image, int width = 256, int height = 256)
    {
        image.Mutate(x => x.Resize(width, height));
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

    public async Task<Image> Base64ToImage(string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        using var ms = new MemoryStream(bytes);
        return await Image.LoadAsync(ms);
    }
}