using System.ComponentModel.DataAnnotations.Schema;

namespace UtopiaWeb.Dto;


//     [Column("id")] public int Id { get; set; }
//     [Column("name")] public string Name { get; set; } 
//     [Column("safe_name")] public string SafeName { get; set; } 
//     [Column("priv")] public int Priv { get; set; }
//     [Column("pw_bcrypt")] public string Password { get; set; }
//     [Column("country")] public string Country { get; set; }
//     [Column("silence_end")] public string SilenceEnd { get; set; }
//     [Column("donor_end")] public string DonorEnd { get; set; }
//     [Column("latest_activity")] public string LatestActivity { get; set; }
//     [Column("clan_id")] public int ClanId{ get; set; }
//     [Column("clan_priv")] public int ClanPriv { get; set; }
//     [Column("preferred_mode")] public int PreferredMode { get; set; }
//     [Column("play_style")] public int PlayStyle { get; set; }
//     [Column("custom_badge_name")] public string CustomBadgeName { get; set; }
//     [Column("custom_badge_icon")] public string CustomBadgeIcon { get; set; }
//     [Column("userpage_content")] public string UserpageContent { get; set; }

[Table("users")]
public class UserDto
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Name { get; set; } = null!;
    [Column("pw_bcrypt")] public string Password { get; set; } = null!;
    [Column("safe_name")] public string SafeName { get; set; } = null!; 
    [Column("userpage_content")] public string? UserpageContent { get; set; }
    // [Column("badges")] public string? Badges { get; set; }

    


    //
     
}