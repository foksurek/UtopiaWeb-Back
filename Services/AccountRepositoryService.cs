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
    
    public async Task<bool> AccountExists(string username)
    {
        return await dbContext.Users.AnyAsync(x => x.Name == username);
    }
    
    public async Task<bool> AccountExists(int id)
    {
        return await dbContext.Users.AnyAsync(x => x.Id == id);
    }

    public async Task ChangeUsername(int id, string newUsername)
    {
        var dbUser = await dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        dbUser!.Name = newUsername;
        dbUser.SafeName = newUsername.ToLower().Replace(" ", "_");
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<BadgeDto>> GetBadges(int id)
    {
        return await dbContext.PlayerBadges
            .Include(b => b.Badge)
            .Where(x => x.UserId == id)
            .Select(x => x.Badge)
            .ToListAsync();
    }
}