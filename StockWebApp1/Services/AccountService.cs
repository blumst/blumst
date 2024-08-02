using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StockWebApp1.DTO;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;
using System.Security.Claims;

namespace StockWebApp1.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto registerModel)
        {
            var user = _mapper.Map<User>(registerModel);
            user.DateCreated = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            return result;
        }
        public async Task<string> LoginAsync(LoginDto loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
                return null!;

            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            return await _tokenService.GenerateJWTToken(user!);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> GetProfileAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
    }
}
