using Microsoft.EntityFrameworkCore;
using UtopiaWeb.Dto;

namespace UtopiaWeb.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserDto> Users { get; set; } = null!;
}