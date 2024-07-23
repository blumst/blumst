using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userRepository.GetAllAsync(); 
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { Message = "User has not been found." });

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var routeValues = new { id = user.Id };

            return CreatedAtAction(nameof(GetById), routeValues, user);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
