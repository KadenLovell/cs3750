using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : Controller {
        private readonly DataContext _context;
        private readonly AssignmentService _assignmentService;
        public AssignmentController(DataContext context, AssignmentService assignmentService) {
            _context = context;
            _assignmentService = assignmentService;
        }

        [HttpGet("list/{courseId:long}")]
        public async Task<IActionResult> GetAssignemntsAsync(long courseId) {
            var result = await _assignmentService.GetAssignmentsAsync(courseId);
            return Json(result);
        }

        [HttpGet("{assignmentId:long}")]
        public async Task<IActionResult> GetAssignmentAsync(long assignmentId) {
            var result = await _assignmentService.GetAssignmentAsync(assignmentId);
            return Json(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAssignmentAsync([FromBody] dynamic model) {
            var result = await _assignmentService.AddAssignmentAsync(model);
            return Json(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAssignmentAsync([FromBody] dynamic model) {
            var result = await _assignmentService.UpdateAssignmentAsync(model);
            return Json(result);
        }
    }
}