using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;
//rename to add services
namespace Server.Services {

    public class UserCourseService {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserCourses> _repository;
        private readonly IRepository<UserCourses> _userCoursesRepository;
        public UserCourseService(IHttpContextAccessor httpContextAccessor, IRepository<UserCourses> repository) {
            _repository = repository;
            // _userCoursesRepository = _userCoursesRepository;
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
                    userId = userCourse.UserID
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
            //if (model.studentId != null, model.courseId != null){
            //i need to prevent student from registering again
            //if(userID = )
            int studentID = (int)model.studentId;
            string courseID = (string)model.courseId;

            var result2 = await _repository.CheckDuplicateEntry(studentID, courseID);
            if (result2 != null) {
                //send better message than false "dupliacte classs"
                return false;
            }

            // var course = await _courseRepository.GetCourseByIdAsync(Convert.ToInt64(courseID));

            // if userList.Size >= course.Capacity {
            // return "class is full"
            // }
            var userCourse = new UserCourses {
                UserID = model.studentId,
                CourseID = model.courseId,
                CreatedDate = DateTime.Now, // DateTime.Parse((string)model.startTime),
                ModifiedDate = DateTime.Now // DateTime.Parse((string)model.endTime),
            };
            await _repository.AddAsync(userCourse);

            // }

            // var userCourse = new UserCourse {
            //     Course = model.course,
            //     User = model.user,
            // };

            // await _repository.AddAsync(user);

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