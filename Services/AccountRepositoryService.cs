using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Contexts;
using UtopiaWeb.Dto;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Services;

public class AccountRepositoryService(AppDbContext dbContext) : IAccountRepositoryService
{
    public async Task<UserDto?> GetAccount(string username)
    {
        return await dbContext.Users.Where(x => x.Name == username).FirstOrDefaultAsync();
    }

    public async Task<UserDto?> GetAccount(int id)
    {
        return await dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task ChangeUsername(int id, string newUsername)
    {
        var dbUser = await dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        dbUser!.Name = newUsername;
        await dbContext.SaveChangesAsync();
    }
}