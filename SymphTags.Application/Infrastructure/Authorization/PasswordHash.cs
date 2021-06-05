using System.Security.Cryptography;
using System.Text;

namespace SymphTagsApp.Application.Infrastructure.Authorization
{
    public class PasswordHash
    {
        public static byte[] CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();

            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            var hmac = new HMACSHA512(salt);
            
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }

    }
}
