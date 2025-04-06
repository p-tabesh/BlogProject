using System.Security.Cryptography;
using System.Text;

namespace Blog.Domain.ValueObject;

public class Password
{
    public string Value { get; private set; }

    private Password() { }

    private Password(string password)
    {
        var hashedPassword = HashPassword(password);
        Value = hashedPassword;
    }

    public static Password CreateForRegister(string inputPassword)
    {
        if (!IsSecurePassword(inputPassword))
            throw new ArgumentException("Password insecure");

        var password = new Password(inputPassword);
        return password;
    }

    public static Password CreateForLogin(string inputPassword) => new Password(inputPassword);

    private static bool IsSecurePassword(string password)
    {
        if (!string.IsNullOrEmpty(password) && password.Contains("@") && password.Length > 8)
            return true;

        return false;
    }

    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashedPassword = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
