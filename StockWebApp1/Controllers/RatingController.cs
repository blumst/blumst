using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : BaseController<Rating, RatingService, RatingDto>
    {
        public RatingController(RatingService service) : base(service) { }
    }
}
