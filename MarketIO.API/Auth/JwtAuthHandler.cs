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

        public string CreateToken(Customers customer , string Role)
        {
            JwtSettings _settings = new JwtSettings();
            
            _configuration.GetSection(nameof(JwtSettings)).Bind(_settings);
            var tokenHendler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.Name, customer.UserName),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Audience = _settings.Audience,
                Issuer = _settings.Issuer,
                Expires = DateTime.Now.AddDays(_settings.Expired),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Secret)) ,SecurityAlgorithms.HmacSha256)
                
            };

            var token = tokenHendler.CreateToken(tokenDescriptor);
            return tokenHendler.WriteToken(token);
        }
    }
}
