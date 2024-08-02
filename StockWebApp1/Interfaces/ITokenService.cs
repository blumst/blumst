using StockWebApp1.Models;

namespace StockWebApp1.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJWTToken(User user);
    }
}
