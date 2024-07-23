using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagController(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tag = await _tagRepository.GetAllAsync();
            return Ok(tag);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
                return NotFound(new { Message = "Tag has not been found." });

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();

            var routeValues = new { id = tag.Id };

            return CreatedAtAction(nameof(GetById), routeValues, tag);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Tag tag)
        {
            if (id != tag.Id)
                return BadRequest();

            _tagRepository.Update(tag);
            await _tagRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
                return NotFound();

            await _tagRepository.DeleteAsync(id);
            await _tagRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
