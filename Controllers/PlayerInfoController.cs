using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PlayerInfoController(
    IScoreRepositoryService scoresRepository,
    IBeatmapRespositoryService beatmapRepository,
    IHttpResponseJsonService responseService,
    IAccountRepositoryService accountRepositoryService
) : ControllerBase
{
    [HttpGet("GetBadges")]
    public async Task<IActionResult> GetBadges(int id)
    {
        if (!await accountRepositoryService.AccountExists(id)) return NotFound(responseService.NotFound("User not found"));
        var badges = await accountRepositoryService.GetBadges(id);
        var data = new
        {
            badges = badges.Select(x => new
            {
                id = x.Id,
                description = x.Description
            })
        };
        return Ok(responseService.Ok(data));
    }
    
    [HttpGet("GetScores")]
    public async Task<IActionResult> GetScores(int id, int mode, string scope, int limit = 20, int offset = 0)
    {
        if (scope != "best" && scope != "recent") 
            return BadRequest(responseService.BadRequest(["Invalid scope parameter"]));
    
        var best = scope == "best";
        var scoresWithBeatmaps = await scoresRepository.GetPlayerTopWithBeatmaps(id, mode, best, limit, offset);
    
        var result = scoresWithBeatmaps.Select(tuple => new
        {
            id = tuple.Score.Id,
            score = tuple.Score.Score,
            pp = Math.Round(tuple.Score.Pp, 2),
            acc =  Math.Round(tuple.Score.Acc, 2),
            max_combo = tuple.Score.MaxCombo,
            mods = tuple.Score.Mods,
            n300 = tuple.Score.N300,
            n100 = tuple.Score.N100,
            n50 = tuple.Score.N50,
            nmiss = tuple.Score.NMiss,
            ngeki = tuple.Score.NGeki,
            nkatu = tuple.Score.NKatu,
            grade = tuple.Score.Grade,
            status = tuple.Score.Status,
            mode = tuple.Score.Mode,
            play_time = tuple.Score.PlayTime,
            time_elapsed = tuple.Score.TimeElapsed,
            perfect = tuple.Score.Perfect,
            beatmap = new
            {
                md5 = tuple.Beatmap.Md5,
                id = tuple.Beatmap.Id,
                set_id = tuple.Beatmap.SetId,
                artist = tuple.Beatmap.Artist,
                title = tuple.Beatmap.Title,
                version = tuple.Beatmap.Version,
                creator = tuple.Beatmap.Creator,
                last_update = tuple.Beatmap.LastUpdate,
                total_length = tuple.Beatmap.TotalLength,
                max_combo = tuple.Beatmap.MaxCombo,
                status = tuple.Beatmap.Status,
                plays = tuple.Beatmap.Plays,
                passes = tuple.Beatmap.Passes,
                mode = tuple.Beatmap.Mode,
                bpm = tuple.Beatmap.Bpm,
                cs =  Math.Round(tuple.Beatmap.CircleSize, 2),
                od =  Math.Round(tuple.Beatmap.OverallDifficulty, 2),
                ar =  Math.Round(tuple.Beatmap.ApproachRate, 2),
                hp =  Math.Round(tuple.Beatmap.HealthDrain, 2),
                diff =  Math.Round(tuple.Beatmap.Difficulty, 2),
            }
        });

        return Ok(responseService.Ok(result));
    }
}