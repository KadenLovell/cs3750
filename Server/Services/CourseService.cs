using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class CourseService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Course> _repository;
        private readonly IRepository<User> _userRepository;

        public CourseService(IHttpContextAccessor httpContextAccessor, IRepository<Course> repository, IRepository<User> userRepository) {
            _repository = repository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetCoursesAsync() {
            var courses = await _repository.GetCoursesAsync();
            if (courses == null || courses.Count == 0) {
                return null;
            }

            var result = new List<dynamic>();

            foreach (var course in courses) {
                result.Add(new {
                    course.Id,
                    course.Name,
                    course.CreditHours,
                    course.Department,
                    course.Capacity,
                    course.Instructor,
                    course.StartTime,
                    course.EndTime,
                    course.Code
                });
            }

            return result;
        }

        public async Task<dynamic> GetCourseAsync(long id) {
            var courses = await _repository.GetCourseByIdAsync(id);

            var result = new {
                id = courses.Id,
                code = courses.Code,
                name = courses.Name,
                description = courses.Description,
                department = courses.Department,
                creditHours = courses.CreditHours,
                location = courses.Location,
                startTime = courses.StartTime?.ToString("d"),
                endTime = courses.EndTime?.ToString("d"),
                capacity = courses.Capacity
            };

            return result;
        }

        public async Task<dynamic> AddCourseAsync(dynamic model) {
            var user = await _userRepository.GetUserById(_httpContextAccessor.HttpContext.User.Identity.Id());
            if (user == null) {
                return null;
            }

            if (user.Role) {
                var course = new Course {
                    CreatedById = user.Id,
                    Instructor = $"{user.LastName}, {user.FirstName}",
                    Code = model.code,
                    Name = model.name,
                    Description = model.description,
                    Department = model.department,
                    CreditHours = model.creditHours,
                    Location = model.location,
                    StartTime = null, // DateTime.Parse((string)model.startTime),
                    EndTime = null, // DateTime.Parse((string)model.endTime),
                    Capacity = model.capacity,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                };

                await _repository.AddAsync(course);

                var result = new {
                    success = true,
                    coursees = new {
                        id = course.Id,
                        code = course.Code,
                        name = course.Name,
                        description = course.Description,
                        department = course.Department,
                        creditHours = course.CreditHours,
                        location = course.Location,
                        capacity = course.Capacity
                    }
                };

                return result;
            } else {
                return null;
            }
        }

        public async Task<dynamic> UpdateCourseAsync(dynamic model) {
            var user = await _userRepository.GetUserById(_httpContextAccessor.HttpContext.User.Identity.Id());

            if (user == null) {
                return null;
            }

            var course = await _repository.GetCourseByIdAsync((long)model.id);

            if (course == null) {
                return null;
            }

            if (user.Role && user.Id == course.CreatedById) {
                course.Code = model.code;
                course.Name = model.Name;
                course.Description = model.description;
                course.Department = model.department;
                course.CreditHours = model.creditHours;
                course.Location = model.location;
                course.StartTime = DateTime.Parse((string)model.startTime);
                course.EndTime = DateTime.Parse((string)model.endTime);
                course.Capacity = model.capacity;

                await _repository.UpdateAsync(course);

                var result = new {
                    success = true,
                    id = course.Id,
                    code = course.Code,
                    name = course.Name,
                    description = course.Description,
                    department = course.Department,
                    creditHours = course.CreditHours,
                    location = course.Location,
                    startTime = course.StartTime?.ToString("d"),
                    endTime = course.EndTime?.ToString("d"),
                    capacity = course.Capacity,
                };

                return result;
            } else {
                return null;
            }
        }

        public async Task<dynamic> GetCourseSearchAsync(dynamic model) {
            var courses = await _repository.SearchCourses((string)model.name, (string)model.department, (string)model.instructor);
            if (courses == null || courses.Count == 0) {
                return null;
            }

            var result = new List<dynamic>();

            foreach (var course in courses) {
                result.Add(new {
                    course.Id,
                    course.Name,
                    course.CreditHours,
                    course.Department,
                    course.Capacity,
                    course.Instructor,
                    course.StartTime,
                    course.EndTime,
                    course.Code
                });
            }

            return result;
        }
    }
}