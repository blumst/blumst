using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Extensions;
using StockWebApp1.Interfaces;

namespace StockWebApp1.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registration")]
        public  async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.RegisterAsync(registerModel);

            return !result.Succeeded
                ? new BadRequestObjectResult("There was an error processing your request, please try again")
                : new OkObjectResult("User Register Successfully");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _accountService.LoginAsync(loginModel);

            return string.IsNullOrEmpty(token)
                ? new BadRequestObjectResult("Invalid login attempt")
                : new OkObjectResult(new {Token = token});
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return Ok("Logout Successful");
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _accountService.GetProfileAsync(User);

            user.EnsureFound("User not found");

            return Ok(user);
        }
    }
}
