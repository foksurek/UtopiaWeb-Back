using Microsoft.AspNetCore.Mvc;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PlayerStats(
    IScoreRepositoryService scoresRepository,
    IBeatmapRespositoryService beatmapRepository,
    IHttpResponseJsonService responseService
    ) : ControllerBase
{
    
    [HttpGet("GetScores")]
    public async Task<IActionResult> GetScores(int id, int mode, string scope, int limit = 20, int offset = 0)
    {
        if (scope != "best" && scope != "recent") return BadRequest(responseService.BadRequest(["Invalid scope parameter"]));
        var best = scope == "best";
        var scores = await scoresRepository.GetPlayerTop(id, mode, best, limit, offset);
        var result = new List<object>();
        //TODO: cache beatmaps
        foreach (var score in scores)
        {
            var beatmap = await beatmapRepository.GetBeatmap(score.MapMd5);
            if (beatmap == null) continue;
            result.Add(new
            {
                id = score.Id,
                score = score.Score,
                pp = score.Pp,
                acc = score.Acc,
                max_combo = score.MaxCombo,
                mods = score.Mods,
                n300 = score.N300,
                n100 = score.N100,
                n50 = score.N50,
                nmiss = score.NMiss,
                ngeki = score.NGeki,
                nkatu = score.NKatu,
                grade = score.Grade,
                status = score.Status,
                mode = score.Mode,
                play_time = score.PlayTime,
                time_elapsed = score.TimeElapsed,
                perfect = score.Perfect,
                beatmap = new
                {
                    md5 = beatmap.Md5,
                    id = beatmap.Id,
                    set_id = beatmap.SetId,
                    artist = beatmap.Artist,
                    title = beatmap.Title,
                    version = beatmap.Version,
                    creator = beatmap.Creator,
                    last_update = beatmap.LastUpdate,
                    total_length = beatmap.TotalLength,
                    max_combo = beatmap.MaxCombo,
                    status = beatmap.Status,
                    plays = beatmap.Plays,
                    passes = beatmap.Passes,
                    mode = beatmap.Mode,
                    bpm = beatmap.Bpm,
                    cs = beatmap.CircleSize,
                    od = beatmap.OverallDifficulty,
                    ar = beatmap.ApproachRate,
                    hp = beatmap.HealthDrain,
                    diff = beatmap.Difficulty,
                }
            });
        }
        return Ok(responseService.Ok(result));
    }
}