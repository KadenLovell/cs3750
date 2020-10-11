using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller {
        private readonly DataContext _context;
        private readonly ClassService _classService;
        public ClassController(DataContext context, ClassService classService) {
            _context = context;
            _classService = classService;
        }

        [HttpGet("{classId:long}")]
        public async Task<IActionResult> GetClassAsync(long classId) {
            var result = await _classService.GetClassAsync(classId);
            return Json(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddClassAsync([FromBody] dynamic model) {
            var result = await _classService.AddClassAsync(model);
            return Json(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateClassAsync([FromBody] dynamic model) {
            var result = await _classService.UpdateClassAsync(model);
            return Json(result);
        }
    }
}