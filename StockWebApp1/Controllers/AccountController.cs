using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterUser")]
        public  async Task<IActionResult> Register([FromBody] User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User { UserName = model.Email, Email = model.Email, DateCreated = DateTime.UtcNow };
                    var result = await _userManager.CreateAsync(user, model.PasswordHash);

                    return !result.Succeeded
                        ? new BadRequestObjectResult("There was an error processing your request, please try again.")
                        : new OkObjectResult("User Register Successfully");
                }

                return BadRequest(ModelState);

            } catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordHash, isPersistent: true, lockoutOnFailure: false);

                    return !result.Succeeded
                            ? new BadRequestObjectResult("Invalid login attempt.")
                            : new OkObjectResult("Login Successful");
                }
                return BadRequest(ModelState);

            } catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("Logout Successful");
        }

        [HttpGet]
        [Route("Profile")]
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
