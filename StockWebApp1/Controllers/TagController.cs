using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagRepository) => _tagService = tagRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var tag = await _tagService.GetAllTagAsync(cancellationToken);
            return Ok(tag);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var tag = await _tagService.GetTagByIdAsync(id, cancellationToken);
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagDto tagDto, CancellationToken cancellationToken)
        {
            await _tagService.CreateTagAsync(tagDto, cancellationToken);
            return Ok(tagDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TagDto tagDto, CancellationToken cancellationToken)
        {
            await _tagService.UpdateTagAsync(id, tagDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _tagService.DeleteTagAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
