using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registration")]
        public  async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(model);
            user.DateCreated = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            return !result.Succeeded
                ? new BadRequestObjectResult("There was an error processing your request, please try again.")
                : new OkObjectResult("User Register Successfully");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: true, lockoutOnFailure: false);

            return !result.Succeeded
                ? new BadRequestObjectResult("Invalid login attempt.")
                : new OkObjectResult("Login Successful");
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("Logout Successful");
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
    }
}
