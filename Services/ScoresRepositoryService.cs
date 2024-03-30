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

    public async Task<IEnumerable<ScoreDto>> GetPlayerTop(int userId, int mode, bool best = false, int limit = 20, int offset = 0)
    {
        if (best) 
            return await dbContext.Scores
                .Where(s => s.UserId == userId && s.Mode == mode && s.Status == 2)
                .OrderByDescending(s => s.Pp)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        return await dbContext.Scores
            .Where(s => s.UserId == userId && s.Mode == mode)
            .OrderByDescending(s => s.PlayTime)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<(ScoreDto Score, BeatmapDto Beatmap)>> GetPlayerTopWithBeatmaps(int userId, int mode, bool best = false, int limit = 20, int offset = 0)
    {
        IQueryable<ScoreDto> query = dbContext.Scores.Where(s => s.UserId == userId && s.Mode == mode);
    
        if (best) 
        {
            query = query.Where(s => s.Status == 2)
                .OrderByDescending(s => s.Pp);
        }
        else
        {
            query = query.OrderByDescending(s => s.PlayTime);
        }

        var result = await query.Skip(offset)
            .Take(limit)
            .Join(dbContext.Beatmaps,
                score => score.MapMd5,
                beatmap => beatmap.Md5,
                (score, beatmap) => new { Score = score, Beatmap = beatmap })
            .ToListAsync();

        return result.Select(r => (r.Score, r.Beatmap));
    }
}