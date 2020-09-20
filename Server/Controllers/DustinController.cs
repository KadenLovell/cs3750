using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DustinController : Controller {
        private readonly DataContext _context;
        private readonly DustinService _dustinService;
        public DustinController(DataContext context, DustinService dustinService) {
            _context = context;
            _dustinService = dustinService;
        }

        [HttpPost("")]
        public async Task<IActionResult> MyMethodAsync([FromBody] dynamic model) {
            return Json(null);
        }
    }
}