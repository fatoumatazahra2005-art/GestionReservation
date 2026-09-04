using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webapisecond.Models;

namespace webapisecond.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenererToken(Utilisateur utilisateur)
        {
            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    utilisateur.Email
                ),

                new Claim(
                    ClaimTypes.Email,
                    utilisateur.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    utilisateur.Role
                ),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                )
            };

            var cle = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                cle,
                SecurityAlgorithms.HmacSha256
            );

            var duree = int.Parse(
                _config["Jwt:DureeValiditeMinutes"]!
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duree),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}