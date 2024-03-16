using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Contexts;
using UtopiaWeb.Dto;

namespace UtopiaWeb.Services;

public class AuthService(AppDbContext dbContext)
{
    public async Task<bool> CheckSession(HttpContext context, ClaimsPrincipal user)
    {
        if (user.Identity?.IsAuthenticated != true) return false;
        Console.WriteLine(user.Identity.Name);
        var account = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == user.Identity.Name);
        if (account == null) return false;
        Console.WriteLine(user.Identity.Name);
        if (account.Id.ToString() != user.FindFirst("Id")?.Value) return false;
        await SignInAsync(context, account);
        return true;
    }
    
    public async Task SignInAsync(HttpContext context, UserDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name),
            new("Id", user.Id.ToString()),
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await context.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(7)
        });
    }
}