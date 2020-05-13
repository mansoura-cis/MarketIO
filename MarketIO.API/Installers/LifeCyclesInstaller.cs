using MarketIO.API.Auth;
using MarketIO.BLL.Repositories;
using MarketIO.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MarketIO.API.Installers
{
    public class LifeCyclesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJwtAuthHandler, JwtAuthHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
