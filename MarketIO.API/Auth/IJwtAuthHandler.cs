using MarketIO.DAL.Domain;

namespace MarketIO.API.Auth
{
    public interface IJwtAuthHandler
    {
        public string GetSecurityToken(string Email, string Username, string Role);
    }
}
