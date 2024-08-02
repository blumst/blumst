using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService) => _ratingService = ratingService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var rating = await _ratingService.GetAllRatingAsync(cancellationToken);
            return Ok(rating);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id, cancellationToken);
            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingDto ratingDto, CancellationToken cancellationToken)
        {
            await _ratingService.CreateRatingAsync(ratingDto, cancellationToken);
            return Ok(ratingDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RatingDto ratingDto, CancellationToken cancellationToken)
        {
            await _ratingService.UpdateRatingAsync(id, ratingDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _ratingService.DeleteRatingAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
