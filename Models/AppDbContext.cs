using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Dto;

namespace UtopiaWeb.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserDto> Users { get; set; } = null!;
    public DbSet<BeatmapDto> Beatmaps { get; set; } = null!;
    public DbSet<ScoreDto> Scores { get; set; } = null!;
}