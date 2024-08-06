using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentController : BaseController<Comment, CommentService, CommentDto>
    {
        public CommentController(CommentService service) : base(service) { }
    }
}
