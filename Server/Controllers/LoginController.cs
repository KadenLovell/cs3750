using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller {
        private readonly DataContext _context;
        private readonly LoginService _loginService;
        public LoginController(DataContext context, LoginService loginService) {
            _context = context;
            _loginService = loginService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> LoginAsync([FromBody] dynamic model) {
            var result = await _loginService.LoginAsync(model);
            return Json(result);
        }
    }
}