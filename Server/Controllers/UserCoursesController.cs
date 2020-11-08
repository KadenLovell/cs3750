using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using Server.Services;

namespace Server.Controllers {
    [Route("api/[controller]")]
   // /api/UserCourses/add

    [ApiController]
    public class UserCoursesController : Controller{
        private readonly DataContext _context;
        private readonly UserCourseService _userCoursesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserCoursesController(DataContext context, UserCourseService userCoursesService, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _userCoursesService = userCoursesService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserCourseAsync([FromBody] dynamic model) {
            var result = await _userCoursesService.AddUserCourseAsync(model);
            return Json(result);
        }




    }
}