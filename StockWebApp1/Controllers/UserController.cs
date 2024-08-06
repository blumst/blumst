using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockWebApp1.DTO;
using StockWebApp1.Models;

namespace StockWebApp1.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController<User, UserService, UserDto>
    {
        public UserController(UserService service) : base(service) { }
    }
}
