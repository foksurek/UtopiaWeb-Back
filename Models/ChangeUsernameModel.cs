using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace UtopiaWeb.Models;

public class ChangeUsernameModel
{
    [RegularExpression(@"^[a-zA-Z0-9_ ]{3,32}$", ErrorMessage = "Username must be between 3 and 32 characters and can only contain letters, numbers, spaces and underscores")]
    public string NewUsername { get; set; } = null!;
}