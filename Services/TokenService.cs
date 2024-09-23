using api.Extensions;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public class TokenService
    {
        public string GenerateToken(int msExpire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            if (key.Length < 32)
            {
                key = PadKeyTo32Bytes(key);
            }

            var expireToken = DateTime.UtcNow.AddMilliseconds(msExpire);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expireToken,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static byte[] PadKeyTo32Bytes(byte[] key)
        {
            if (key.Length >= 32)
            {
                return key;
            }

            byte[] paddedKey = new byte[32];
            Buffer.BlockCopy(key, 0, paddedKey, 0, key.Length);
            return paddedKey;
        }

    }
}
