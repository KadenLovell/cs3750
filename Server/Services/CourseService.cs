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
        public CourseService(IHttpContextAccessor httpContextAccessor, IRepository<Course> repository) {
            _repository = repository;
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
            var coursees = await _repository.GetCourseByIdAsync(id);

            var result = new {
                id = coursees.Id,
                code = coursees.Code,
                name = coursees.Name,
                description = coursees.Description,
                department = coursees.Department,
                creditHours = coursees.CreditHours,
                location = coursees.Location,
                startTime = coursees.StartTime?.ToString("d"),
                endTime = coursees.EndTime?.ToString("d"),
                capacity = coursees.Capacity
            };

            return result;
        }

        public async Task<dynamic> AddCourseAsync(dynamic model) {
            var course = new Course {
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
        }

        public async Task<dynamic> UpdateCourseAsync(dynamic model) {
            var course = await _repository.GetCourseByIdAsync((long)model.id);

            if (course == null) {
                return null;
            }

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