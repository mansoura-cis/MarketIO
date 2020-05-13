using MarketIO.DAL.Domain;

namespace MarketIO.API.Auth
{
    public interface IJwtAuthHandler
    {
        public string CreateToken(Customers customer, string Role);
    }
}
