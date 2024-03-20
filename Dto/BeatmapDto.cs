using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtopiaWeb.Dto;
[Table("maps")]
public class BeatmapDto
{
    [Column("server")]
    public string Server { get; set; } = null!;
    
    [Key] 
    [Column("id")]
    public int Id { get; set; }
    
    [Column("set_id")]
    public int SetId { get; set; }
    
    [Column("status")]
    public int Status { get; set; }
    
    [Column("md5")]
    public string Md5 { get; set; } = null!;
    
    [Column("artist")]
    public string Artist { get; set; } = null!;
    
    [Column("title")]
    public string Title { get; set; } = null!;
    
    [Column("version")]
    public string Version { get; set; } = null!;
    
    [Column("creator")]
    public string Creator { get; set; } = null!;
    
    [Column("filename")]
    public string Filename { get; set; } = null!;
    
    [Column("last_update")]
    public DateTime LastUpdate { get; set; }
    
    [Column("total_length")]
    public int TotalLength { get; set; }
    
    [Column("max_combo")]
    public int MaxCombo { get; set; }
    
    [Column("frozen")]
    public int Frozen { get; set; }
    
    [Column("plays")]
    public int Plays { get; set; }
    
    [Column("passes")]
    public int Passes { get; set; }
    
    [Column("mode")]
    public int Mode { get; set; }
    
    [Column("bpm")]
    public double Bpm { get; set; }
    
    [Column("cs")]
    public double CircleSize { get; set; }
    
    [Column("od")]
    public double OverallDifficulty { get; set; }
    
    [Column("ar")]
    public double ApproachRate { get; set; }
    
    [Column("hp")]
    public double HealthDrain { get; set; }
    
    [Column("diff")]
    public double Difficulty { get; set; }
}