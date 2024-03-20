using UtopiaWeb.Dto;

namespace UtopiaWeb.Interfaces;

public interface IBeatmapRespositoryService
{
    public Task<BeatmapDto?> GetBeatmap(int id);
    public Task<BeatmapDto?> GetBeatmap(string md5);
}