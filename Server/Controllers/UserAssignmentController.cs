using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserAssignmentController : Controller {
        private readonly DataContext _context;
        private readonly UserAssignmentService _userAssignmentService;
        public UserAssignmentController(DataContext context, UserAssignmentService userAssignmentService) {
            _context = context;
            _userAssignmentService = userAssignmentService;
        }


        [HttpGet("list/{courseId:long}")]
        public async Task<IActionResult> GetUserAssignmentByCourseIdAsync(long courseId) {
            var result = await _userAssignmentService.GetUserAssignmentByCourseIdAsync(courseId);
            return Json(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserAssignmentAsync([FromBody] dynamic model) {
            var result = await _userAssignmentService.AddUserAssignmentAsync(model);
            return Json(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserAssignmentAsync([FromBody] dynamic model) {
            var result = await _userAssignmentService.UpdateUserAssignmentAsync(model);
            return Json(result);
        }

        [HttpPost("grade")]
        public async Task<IActionResult> UpdateUserAssignmentGradeAsync([FromBody] dynamic model) {
            var result = await _userAssignmentService.UpdateUserAssignmentGradeAsync(model);
            return Json(result);
        }

        [HttpGet("delete/{courseId:long}/{assignmentId:long}")]
        public async Task<IActionResult> DeleteUserAssignmentAsync(long courseId, long assignmentId) {
            var result = await _userAssignmentService.DeleteUserAssignmentsAsync(courseId, assignmentId);
            return Json(result);
        }
    }
}