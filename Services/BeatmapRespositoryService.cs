using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Contexts;
using UtopiaWeb.Dto;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Services;

public class BeatmapRespositoryService(AppDbContext dbContext) : IBeatmapRespositoryService
{
    public Task<BeatmapDto?> GetBeatmap(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BeatmapDto?> GetBeatmap(string md5)
    {
        return await dbContext.Beatmaps.FirstOrDefaultAsync(b => b.Md5 == md5);
        
    }
}