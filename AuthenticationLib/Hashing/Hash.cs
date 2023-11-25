using System.Security.Cryptography;
using System.Text;

namespace Hash;

public static class PasswordHasher
{
    public static string Hash(this string password) { 

        byte[] hashed;
        string hashedPassword;

        using (SHA256 sha256 = SHA256.Create())
        {
            hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            hashedPassword = BitConverter.ToString(hashed).Replace("-", "").ToLower();
        }

        return hashedPassword;
    }
}
