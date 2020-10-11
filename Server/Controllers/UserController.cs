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
        [HttpGet("activeuser")]
        public async Task<IActionResult> GetActiveUserAsync() {
            var result = await _userService.GetActiveUserAsync();
            return Json(result);
        }

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetUserAsync(long userId) {
            var result = await _userService.GetUserAsync(userId);
            return Json(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserAsync([FromBody] dynamic model) {
            var result = await _userService.AddUserAsync(model);
            return Json(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] dynamic model) {
            var result = await _userService.UpdateUserAsync(model);
            return Json(result);
        }
    }
}