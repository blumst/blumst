using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/tags")]
    [ApiController]
    public class TagController : BaseController<Tag, TagService, TagDto>
    {
        public TagController(TagService service) : base(service) { }
    }
}
