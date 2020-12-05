using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserCourseService {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserCourses> _repository;
        public UserCourseService(IHttpContextAccessor httpContextAccessor, IRepository<UserCourses> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetUserCourseCoursesAsync() {
            var id = _httpContextAccessor.HttpContext.User.Identity.Id();
            var userCourses = await _repository.GetUserCoursesById(id);

            if (userCourses == null) {
                return null;
            }

            var result = new List<dynamic>();

            foreach (var userCourse in userCourses) {
                result.Add(new {
                    id = userCourse.Id,
                    courseId = Convert.ToInt64(userCourse.CourseID),
                    userId = userCourse.UserID,
                    credits = userCourse.Course.CreditHours,
                    course = new {
                        name = userCourse.Course.Name,
                        code = userCourse.Course.Code,
                        department = userCourse.Course.Department,
                        instructor = userCourse.Course.Instructor,
                        creditHours = userCourse.Course.CreditHours
                    }
                });
            }
            return result;
        }

        public async Task<dynamic> GetUserCourseAsync(long id) {
            var userCourse = await _repository.GetUserCourseById(id);

            var result = new {
                id = userCourse.Id
            };

            return result;
        }

        public async Task<dynamic> AddUserCourseAsync(dynamic model) {
            int studentID = (int)model.studentId;
            long courseID = (long)model.courseId;

            var result2 = await _repository.CheckDuplicateEntry(studentID, courseID);
            if (result2 != null) {
                return false;
            }

            var userCourse = new UserCourses {
                UserID = model.studentId,
                CourseID = model.courseId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            await _repository.AddAsync(userCourse);
            var userCourse2 = await _repository.GetUserCourseById(userCourse.Id);
            var result = new {
                success = true,
                userCourse = new {
                    id = userCourse.Id,
                    user = userCourse.UserID,
                    course = userCourse.CourseID,
                    creditHours = userCourse2.Course.CreditHours
                }
            };

            return result;
        }

        public async Task<dynamic> DeleteUserCourseAsync(long courseId) {
            var studentId = _httpContextAccessor.HttpContext.User.Identity.Id();
            int creditHours = 0;
            dynamic result;
            try {
                var userCourse = await _repository.GetUserCourseByStudentIdAndCourseId(studentId, courseId);
                creditHours = userCourse.Course.CreditHours;
                await _repository.DeleteAsync(userCourse);
            } catch {
                result = new {
                    success = false
                };
            }
            result = new {
                success = true,
                userCourse = new {
                    creditHours
                }
            };
            return result;
        }

    }
}
