using Microsoft.AspNetCore.Mvc;
using StockWebApp1.Interfaces;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> _ratingRepository;

        public RatingController(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rating = await _ratingRepository.GetAllAsync();
            return Ok(rating);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);

            if (rating == null)
                return NotFound(new { Message = "Rating has not been found." });

            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rating rating)
        {
            await _ratingRepository.AddAsync(rating);
            var routeValues = new { id = rating.Id };

            return CreatedAtAction(nameof(GetById), routeValues, rating);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Rating rating)
        {
            if (id != rating.Id)
                return BadRequest();

            await _ratingRepository.UpdateAsync(rating);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _ratingRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await _ratingRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
