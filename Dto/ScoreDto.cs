using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtopiaWeb.Dto;

[Table("scores")]
public class ScoreDto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("map_md5")]
    public string MapMd5 { get; set; } = null!;
    
    [Column("score")]
    public int Score { get; set; }
    
    [Column("pp")]
    public double Pp { get; set; }
    
    [Column("acc")]
    public double Acc { get; set; }
    
    [Column("max_combo")]
    public int MaxCombo { get; set; }
    
    [Column("mods")]
    public int Mods { get; set; }
    
    [Column("n300")]
    public int N300 { get; set; }
    
    [Column("n100")]
    public int N100 { get; set; }
    
    [Column("n50")]
    public int N50 { get; set; }
    
    [Column("nmiss")]
    public int NMiss { get; set; }
    
    [Column("ngeki")]
    public int NGeki { get; set; }
    
    [Column("nkatu")]
    public int NKatu { get; set; }
    
    [Column("grade")]
    public string Grade { get; set; }
    
    [Column("status")]
    public int Status { get; set; }
    
    [Column("mode")]
    public int Mode { get; set; }
    
    [Column("play_time")]
    public DateTime PlayTime { get; set; }
    
    [Column("time_elapsed")]
    public int TimeElapsed { get; set; }
    
    [Column("client_flags")]
    public int ClientFlags { get; set; }
    
    [Column("userid")]
    public int UserId { get; set; }
    
    [Column("perfect")]
    public int Perfect { get; set; }
    
    [Column("online_checksum")]
    public string OnlineChecksum { get; set; } = null!;
    
}