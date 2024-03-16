using System.Security.Cryptography;
using System.Text;
using UtopiaWeb.Contexts;

namespace UtopiaWeb.Services;

public class PasswordService(AppDbContext dbContext)
{
        
    public bool CheckPasswordFromDatabase(string password, string hashedPassword)
    {
        var passwordMd5 = GetMd5Hash(password);
        return BCrypt.Net.BCrypt.Verify(passwordMd5, hashedPassword);
    }
    
    public string HashPassword(string password)
    {
        var passwordMd5 = GetMd5Hash(password);
        return BCrypt.Net.BCrypt.HashPassword(passwordMd5);
    }

    private static string GetMd5Hash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var passwordMd5Bytes = MD5.Create().ComputeHash(passwordBytes);
        return BitConverter.ToString(passwordMd5Bytes).Replace("-", "").ToLower();
    }
}