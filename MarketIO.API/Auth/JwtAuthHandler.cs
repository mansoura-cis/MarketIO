using MarketIO.API.Settings;
using MarketIO.DAL.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketIO.API.Auth
{
    public class JwtAuthHandler : IJwtAuthHandler
    {
        private readonly IConfiguration _configuration;
        public JwtAuthHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSecurityToken(string Email, string Username, string Role)
        {
            JwtSettings _settings = new JwtSettings();
            _configuration.GetSection(nameof(JwtSettings)).Bind(_settings);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, Role),
                }),
                Expires = DateTime.Now.AddMinutes(_settings.Expired),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = DateTime.Now

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
