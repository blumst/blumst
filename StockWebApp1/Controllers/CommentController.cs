using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService) => _commentService = commentService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetAllCommentAsync(cancellationToken);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var comment = await _commentService.GetCommentByIdAsync(id, cancellationToken);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentDto commentDto, CancellationToken cancellationToken)
        {
            await _commentService.CreateCommentAsync(commentDto, cancellationToken);
            return Ok(commentDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CommentDto commentDto, CancellationToken cancellationToken)
        {
            await _commentService.UpdateCommentAsync(id, commentDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _commentService.DeleteCommentAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
