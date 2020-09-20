using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RyanController : Controller {
        private readonly DataContext _context;
        private readonly RyanService _ryanService;
        public RyanController(DataContext context, RyanService ryanService) {
            _context = context;
            _ryanService = ryanService;
        }

        [HttpPost("")]
        public async Task<IActionResult> MyMethodAsync([FromBody] dynamic model) {
            return Json(null);
        }
    }
}