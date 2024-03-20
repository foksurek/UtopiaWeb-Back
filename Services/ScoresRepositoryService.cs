using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Contexts;
using UtopiaWeb.Dto;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Services;

public class ScoresRepositoryService(AppDbContext dbContext) : IScoreRepositoryService
{
    public Task<ScoreDto?> GetScore(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ScoreDto?> GetScore(string md5)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ScoreDto>> GetPlayerTop(int userId, int mode, int status, int limit = 20, int offset = 0)
    {
        var data = await dbContext.Scores
            .Where(s => s.UserId == userId && s.Mode == mode && s.Status == status)
            .OrderByDescending(s => s.Pp)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
        return data;
    }
}