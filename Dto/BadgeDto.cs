using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtopiaWeb.Dto;

[Table("badges")]
public class BadgeDto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    public string Description { get; set; } = null!;
}