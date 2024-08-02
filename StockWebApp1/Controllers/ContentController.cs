using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentService _contentService;

        public ContentController(ContentService contentService) => _contentService = contentService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var content = await _contentService.GetAllContentAsync(cancellationToken);
            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var content = await _contentService.GetContentByIdAsync(id, cancellationToken);
            return Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContentDto contentDto, CancellationToken cancellationToken)
        {
            await _contentService.CreateContentAsync(contentDto, cancellationToken);
            return Ok(contentDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ContentDto contentDto, CancellationToken cancellationToken)
        {
            await _contentService.UpdateContentAsync(id, contentDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _contentService.DeleteContentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
