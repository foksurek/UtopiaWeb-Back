using UtopiaWeb.Dto;

namespace UtopiaWeb.Interfaces;

public interface IScoreRepositoryService
{
    public Task<ScoreDto?> GetScore(int id);
    public Task<ScoreDto?> GetScore(string md5);
    
    public Task<IEnumerable<ScoreDto>> GetPlayerTop(int userId, int mode, int status, int limit = 20, int offset = 0);
}