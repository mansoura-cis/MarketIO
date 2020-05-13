using MarketIO.API.Auth;
using MarketIO.API.Settings;
using MarketIO.BLL.Repositories;
using MarketIO.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketIO.API.Installers
{
    public class SecurityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(schemes =>
            {
                schemes.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                schemes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                schemes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true, 
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateLifetime=true,
                    ValidAudience = jwtSettings.Audience
                };
            
            });

            services.AddAuthorization();



        }
    }
}
