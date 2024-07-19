using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentController(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
                return NotFound(new { Message = "Comment has not been found." });

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
            var routeValues = new { id = comment.Id };

            return CreatedAtAction(nameof(GetById), routeValues, comment);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Comment comment)
        {
            if (id != comment.Id)
                return BadRequest();

            await _commentRepository.UpdateAsync(comment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
                return NotFound();

            await _commentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
