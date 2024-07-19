using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IRepository<Content> _contentRepository;

        public ContentController(IRepository<Content> contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _contentRepository.GetAllAsync();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var content = await _contentRepository.GetByIdAsync(id);

            if (content == null)
                return NotFound(new { Message = "Content has not been found." });

            return Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Content content)
        {
            await _contentRepository.AddAsync(content);
            var routeValues = new { id = content.Id };

            return CreatedAtAction(nameof(GetById), routeValues, content);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Content content)
        {
            if (id != content.Id)
                return BadRequest();

            await _contentRepository.UpdateAsync(content);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _contentRepository.GetByIdAsync(id);

            if (content == null)
                return NotFound();

            await _contentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
