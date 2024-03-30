using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChangelogController(
    IFileService fileService,
    IConfiguration configuration,
    IHttpResponseJsonService httpResponseJsonService
    ): ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetChangelog(string name)
    {
        var path = Path.Combine(configuration["Changelog:FolderPath"]!, name + configuration["Changelog:Extension"]!);
        var text = await fileService.GetTextFromFile(path);
        if (string.IsNullOrEmpty(text)) return NotFound(httpResponseJsonService.NotFound("Changelog not found"));
        return Ok(httpResponseJsonService.Ok(text));
    }
}