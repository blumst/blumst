using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;
using StockWebApp1.Services;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/contents")]
    [ApiController]
    public class ContentController : BaseController<Content, ContentService, ContentDto>
    {
        public ContentController(ContentService service) : base(service) { }

    }
}
