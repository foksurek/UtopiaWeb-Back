using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtopiaWeb.Interfaces;
using UtopiaWeb.Models;
using UtopiaWeb.Services;
using UtopiaWeb.ViewModels;

namespace UtopiaWeb.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class AccountController(
    IAccountRepositoryService accountRepositoryService, 
    PasswordService passwordService,
    AuthService authService,
    IHttpResponseJsonService jsonResponseService,
    IFileService fileService,
    IConfiguration configuration
    ) : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var user = await accountRepositoryService.GetAccount(model.Username);

        if (user == null) return Unauthorized("Wrong password or username");
        
        if (!passwordService.CheckPasswordFromDatabase(model.Password,user.Password)) return Unauthorized("Wrong password or username");
        
        await authService.SignInAsync(HttpContext, user);

        var userData = new
        {
            name = user.Name,
            id = user.Id
            
        };
        
        return Ok(jsonResponseService.Ok(userData));


    }
    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Cookies.Delete("userData");
        return Ok(jsonResponseService.Ok(""));
    }
    
    [Authorize]
    [HttpGet("session")]
    public async Task<IActionResult> Session()
    {
        var userJson = new
        {
            name = User.FindFirst(ClaimTypes.Name)?.Value,
            id = User.FindFirst("Id")?.Value
        };

        if (!await authService.CheckSession(HttpContext, User)) return Unauthorized();
        return Ok(jsonResponseService.Ok(userJson));
    }
    
    
    [Authorize]
    [HttpPost("changeUsername")]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (await accountRepositoryService.GetAccount(model.NewUsername) != null)
            return BadRequest(jsonResponseService.BadRequest(["Username is already taken"]));
        var id = int.Parse(User.FindFirst("Id")?.Value!);
        await accountRepositoryService.ChangeUsername(id, model.NewUsername);
        await authService.SignInAsync(HttpContext, (await accountRepositoryService.GetAccount(id))!);
        return Ok(jsonResponseService.Ok("Username successfully changed"));
    }
    
    
    [Authorize]
    [HttpPost("uploadProfileFiles")]
    public async Task<IActionResult> UploadAvatar(IFormFile? avatar, IFormFile? banner)
    {
        if (avatar == null && banner == null) return BadRequest(jsonResponseService.BadRequest(["No file was uploaded"]));
        if (avatar != null)
        {
            if (avatar.Length > 8000000) return BadRequest(jsonResponseService.BadRequest(["Avatar is too large"]));
            var id = int.Parse(User.FindFirst("Id")?.Value!);
            var fileExtension = avatar.FileName.Split('.').Last();
            await fileService.DeleteFile(configuration["FilePaths:Avatars"]!, id.ToString());
            await fileService.SaveAvatar(configuration["FilePaths:Avatars"]!, id+ "." + fileExtension, avatar);
        }
        if (banner != null)
        {
            if (banner.Length > 8000000) return BadRequest(jsonResponseService.BadRequest(["Banner is too large"]));
            var id = int.Parse(User.FindFirst("Id")?.Value!);
            var fileExtension = banner.FileName.Split('.').Last();
            await fileService.DeleteFile(configuration["FilePaths:Avatars"]!, id + "b");
            await fileService.SaveFile(configuration["FilePaths:Avatars"]!, id + "b" + "." + fileExtension, banner);
        }
        
        return Ok(jsonResponseService.Ok("Profile pictures successfully uploaded"));
    }
}