using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller {
        private readonly DataContext _context;
        private readonly CourseService _courseService;
        public CourseController(DataContext context, CourseService courseService) {
            _context = context;
            _courseService = courseService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCoursesAsync() {
            //var x = await _context.Course.ToListAsync<Course>();
            //x.FindAll(x => x.Name == "Math");
            var result = await _courseService.GetCoursesAsync();
            return Json(result);
        }

        [HttpGet("list/{instructorId:long}")]
        public async Task<IActionResult> GetCoursesByInstructorIdAsync(long instructorId) {
            var result = await _courseService.GetCoursesByInstructorIdAsync(instructorId);
            return Json(result);
        }


        [HttpPost("searchlist")]
        public async Task<IActionResult> GetCourseSearchAsync([FromBody] dynamic model) {
            var result = await _courseService.GetCourseSearchAsync(model);
            return Json(result);
        }

        [HttpGet("{classId:long}")]
        public async Task<IActionResult> GetCourseAsync(long classId) {
            var result = await _courseService.GetCourseAsync(classId);
            return Json(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCourseAsync([FromBody] dynamic model) {
            Course c = new Course();
            c.Name = "Computer Tech";
            //_context.Course.Add(c);
            var result = await _courseService.AddCourseAsync(model);
            return Json(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCourseAsync([FromBody] dynamic model) {
            var result = await _courseService.UpdateCourseAsync(model);
            return Json(result);
        }
    }
}