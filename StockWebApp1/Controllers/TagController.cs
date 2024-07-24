using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagRepository) => _tagService = tagRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tag = await _tagService.GetAllTagAsync();
            return Ok(tag);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagDto tagDto)
        {
            await _tagService.CreateTagAsync(tagDto);
            var routeValues = new { id = tagDto.Id };

            return CreatedAtAction(nameof(GetById), routeValues, tagDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TagDto tagDto)
        {
            await _tagService.UpdateTagAsync(id, tagDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }
    }
}
