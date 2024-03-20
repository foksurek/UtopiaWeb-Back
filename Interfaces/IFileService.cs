namespace UtopiaWeb.Interfaces;

public interface IFileService
{
    public Task SaveFile(string path, string fileName, IFormFile file);
    public Task DeleteFile(string path, string fileName);
    public Task SaveAvatar(string path, string fileName, IFormFile file);
}