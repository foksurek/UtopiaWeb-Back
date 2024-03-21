using SixLabors.ImageSharp;

namespace UtopiaWeb.Interfaces;

public interface IFileService
{
    public Task SaveFile(string path, string fileName, Image image, string type);
    public Task DeleteFile(string path, string fileName);
    public Task<Image> Base64ToImage(string base64);
}