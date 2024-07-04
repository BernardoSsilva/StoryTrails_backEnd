using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoryTrails.JWTAdmin.Services
{
    public class AdminToken
    {

        public string Generate(TokenPayload payload)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("4e594798-6867-4dff-887a-9a7c12882b8e");
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, payload.UserLogin));
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, payload.UserId));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ci,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(2)
            };

            var token = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(token);
            return tokenString;

        }
    }
}