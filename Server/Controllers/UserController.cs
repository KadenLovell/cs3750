using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {
        private readonly DataContext _context;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(DataContext context, UserService userService, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("activeuser")]
        public async Task<IActionResult> GetActiveUserAsync() {
            var result = await _userService.GetActiveUserAsync();
            return Json(result);
        }

        [HttpGet("loaduser")]
        public async Task<IActionResult> GetUserAsync() {
            var id = _httpContextAccessor.HttpContext.User.Identity.Id();
            var result = await _userService.GetUserAsync(id);
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

        [HttpPost("updatefees")]
        public async Task<IActionResult> UpdateFees([FromBody] dynamic model) {
            var result = await _userService.UpdateFees(model);
            return Json(result);
        }

        [HttpPost("updatepaid")]
        public async Task<IActionResult> UpdatePaid([FromBody] dynamic model) {
            var result = await _userService.UpdatePaid(model);
            return Json(result);
        }
    }
}