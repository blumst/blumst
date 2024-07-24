using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllUserAsync(); 
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            await _userService.CreateUserAsync(userDto);
            var routeValues = new { id = userDto.Id };

            return CreatedAtAction(nameof(GetById), routeValues, userDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto userDto)
        {
            await _userService.UpdateUserAsync(id, userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
