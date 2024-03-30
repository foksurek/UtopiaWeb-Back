using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UtopiaWeb.Dto;

[Keyless]
[Table("player_badges")]
public class PlayerBadgeDto
{
    [Column("user_id"), ForeignKey("User")]
    public int UserId { get; set; }
    [Column("badge_id"), ForeignKey("Badge")]
    public int BadgeId { get; set; }
    public BadgeDto Badge { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}