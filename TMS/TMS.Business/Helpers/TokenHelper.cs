using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TMS.Models;
using TMS.Models.Entites;
using TMS.Models.Jwt;

namespace TMS.Business.Helpers
{
    public class TokenHelper
    {
        public static string CreateToken(User user, TokenSettings tokenSettings, DateTime expireDate)
        {
            var securityKey = Convert.FromBase64String(tokenSettings.SecurityKey);
            var claimsIdentity = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Claims = claimsIdentity.Claims.GroupBy(x => x.Type).ToDictionary(y => y.Key, y => (object)y.Select(z => z.Value).ToArray()),
                Issuer = tokenSettings.Issuer,
                Audience = tokenSettings.Issuer,
                Expires = expireDate,
                SigningCredentials = signingCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
