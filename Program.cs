using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Contexts;
using UtopiaWeb.Interfaces;
using UtopiaWeb.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQLConnection")!);
});
builder.Services.AddRouting(options => { options.LowercaseUrls = true; });


builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage))
            .ToList();
        
        var result = new
        {
            Code = 400,
            Message = "Validation errors occurred",
            Details = errors
        };
        
        return new BadRequestObjectResult(result);
    };
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.Name = "UtopiaWeb";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = async context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new 
                { 
                    code = 401,
                    message = "Unauthorized",
                    details = new List<string> { "User is unauthorized" } 
                }));
        };
        options.Events.OnRedirectToLogin = async context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new 
                { 
                    code = 401,
                    message = "Unauthorized",
                    details = new List<string> { "User is unauthorized" } 
                }));
        };
    });


builder.Services.AddTransient<PasswordService>();
builder.Services.AddTransient<IAccountRepositoryService, AccountRepositoryService>();
builder.Services.AddTransient<IBeatmapRespositoryService, BeatmapRespositoryService>();
builder.Services.AddTransient<IScoreRepositoryService, ScoresRepositoryService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<IHttpResponseJsonService, HttpResponseJsonService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("https://borsuq.xyz");
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(new 
            { 
                code = 500,
                message = "Internal server error",
                details = new List<string> { "Something went wrong" } 
            }));
    });
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default", "{controller}/{action}");
app.Run();