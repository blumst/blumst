using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var user = await _userService.GetAllUserAsync(cancellationToken); 
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            await _userService.CreateUserAsync(userDto, cancellationToken);
            return Ok(userDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto userDto, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(id, userDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
