using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;
//rename to add services
namespace Server.Services {
    public class UserCourseService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Course> _repository;
        private readonly IRepository<UserCourse> _userCourseRepository;

        public UserCourseService(IHttpContextAccessor httpContextAccessor, IRepository<UserCourse> repository) {
            _repository = repository;
            _userCourseRepository = userCourseRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetuserCourseCoursesAsync() {
            var userCourseCourses = await _repository.GetUserCoursesById(id);
            _repository.get

            if (userCourse == null) {
                return null;
            }

            var result = new {
                id = _httpContextAccessor.HttpContext.UserCourse.Identity.Id(),
                userCoursename = userCourse.user,
                firstname = userCourse.course,
            };

            return result;
        }

        public async Task<dynamic> GetUserCourseAsync(long id) {
            var userCourse = await _repository.GetUserCourseById(id);

            var result = new {
                id = userCourse.Id,
                user = userCourse.user,
                course = userCourse.course,
                
            };

            return result;
        }

        public async Task<dynamic> AddUserCourseAsync(dynamic model) {
            if (model.username == null || model.email == null) {
                return null;
            }
            //should this be exists by userid and courseid?

            var exists = await _repository.UserCourseExistsById((string)model.username, (string)model.email);

            if (exists) {
                var error = new {
                    errors = new {
                        userCourseExists = true
                    }
                };

                return error;
            }

            var userCourse = new UserCourse {
                Course = model.course,
                User = model.user,
            };

            await _repository.AddAsync(user);

            var result = new {
                success = true,
                userCourse = new {
                    id = userCourse.Id,
                    user = userCourse.User,
                    course = userCourse.Course,
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateUserCourseAsync(dynamic model) {
            var userCourse = await _repository.GetUserCourseById((long)model.id);

            if (userCourse == null) {
                return null;
            }

            userCourse.user = model.user;
            userCourse.course = model.course;

            await _repository.UpdateAsync(userCourse);

            var result = new {
                success = true,
                id = userCourse.Id,
                user = userCourse.User,
                course = userCourse.Course
            };

            return result;
        }
    }
}