using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService) => _commentService = commentService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllCommentAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentDto commentDto)
        {
            await _commentService.CreateCommentAsync(commentDto);
            var routeValues = new { id = commentDto.Id };

            return CreatedAtAction(nameof(GetById), routeValues, commentDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CommentDto commentDto)
        {
            await _commentService.UpdateCommentAsync(id, commentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
