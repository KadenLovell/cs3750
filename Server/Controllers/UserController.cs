using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {
        private readonly DataContext _context;
        private readonly UserService _userService;
        public UserController(DataContext context, UserService userService) {
            _context = context;
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserAsync([FromBody] dynamic model) {
            var result = await _userService.AddUserAsync(model);
            return Json(result);
        }
    }
}