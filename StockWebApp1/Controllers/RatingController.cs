using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService) => _ratingService = ratingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rating = await _ratingService.GetAllRatingAsync();
            return Ok(rating);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingDto ratingDto)
        {
            await _ratingService.CreateRatingAsync(ratingDto);
            var routeValues = new { id = ratingDto.Id };

            return CreatedAtAction(nameof(GetById), routeValues, ratingDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RatingDto ratingDto)
        {
            await _ratingService.UpdateRatingAsync(id, ratingDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ratingService.DeleteRatingAsync(id);
            return NoContent();
        }
    }
}
