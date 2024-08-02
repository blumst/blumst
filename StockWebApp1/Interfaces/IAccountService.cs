using Microsoft.AspNetCore.Identity;
using StockWebApp1.DTO;
using StockWebApp1.Models;
using System.Security.Claims;

namespace StockWebApp1.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto registerModel);
        Task<string> LoginAsync(LoginDto loginModel);
        Task LogoutAsync();
        Task<User> GetProfileAsync(ClaimsPrincipal user);
    }
}
