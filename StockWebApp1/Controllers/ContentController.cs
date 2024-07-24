using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentService _contentService;

        public ContentController(ContentService contentService) => _contentService = contentService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var content = await _contentService.GetAllContentAsync();
            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var content = await _contentService.GetContentByIdAsync(id);
            return Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContentDto contentDto)
        {
            await _contentService.CreateContentAsync(contentDto);
            var routeValues = new { id = contentDto.Id };

            return CreatedAtAction(nameof(GetById), routeValues, contentDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ContentDto contentDto)
        {
            await _contentService.UpdateContentAsync(id, contentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contentService.DeleteContentAsync(id);
            return NoContent();
        }
    }
}
