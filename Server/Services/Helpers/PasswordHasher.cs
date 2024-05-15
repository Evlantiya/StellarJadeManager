using System.Security.Cryptography;

namespace StellarJadeManager.Server.Services
{

    public static class PasswordHasher
    {
        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return rfc2898DeriveBytes.GetBytes(32); // Creates a 256-bit hash
            }
        }
    }

}
