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
                    course = new {
                        userCourse.Course.Name,
                        userCourse.Course.Description                    
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
            var studentID = (long)model.studentId;
            var courseID = (long)model.courseId;

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

            var result = new {
                success = true,
                userCourse = new {
                    id = userCourse.Id,
                    user = userCourse.UserID,
                    course = userCourse.CourseID,
                }
            };

            return result;
        }

        public async Task<dynamic> DeleteUserCourseAsync(long id) {
            dynamic result;
            try {
                var userCourse = await _repository.GetUserCourseById(id);
                await _repository.DeleteAsync(userCourse);
            } catch {
                result = new {
                    success = false
                };
            }
            result = new {
                success = true
            };
            return result;
        }
        // public async Task<dynamic> UpdateUserCourseAsync(dynamic model) {
        // var userCourse = await _repository.GetUserCourseById((long)model.id);

        // if (userCourse == null) {
        //     return null;
        // }

        // userCourse.user = model.user;
        // userCourse.course = model.course;

        // await _repository.UpdateAsync(userCourse);

        // var result = new {
        // success = true
        // id = userCourse.Id,
        // user = userCourse.User,
        // course = userCourse.Course
        // };

        // return result;
        // }
    }
}