using UtopiaWeb.Dto;

namespace UtopiaWeb.Interfaces;

public interface IAccountRepositoryService
{
    public Task<UserDto?> GetAccount(string username);
    public Task<UserDto?> GetAccount(int id);
    
    public Task ChangeUsername(int id, string newUsername);
    
}