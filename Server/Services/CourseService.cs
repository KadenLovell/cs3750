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
                    course.StartDate,
                    course.StartTime,
                    course.EndTime,
                    course.Code,
                    course.Monday,
                    course.Tuesday,
                    course.Wednesday,
                    course.Thursday,
                    course.Friday
                });
            }

            return result;
        }

        public async Task<dynamic> GetCoursesByInstructorIdAsync(long instructorId) {
            var courses = await _repository.GetCoursesByInstructorIdAsync(instructorId);
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
                    course.StartDate,
                    course.StartTime,
                    course.EndTime,
                    course.Code,
                    course.Monday,
                    course.Tuesday,
                    course.Wednesday,
                    course.Thursday,
                    course.Friday
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
                startDate = courses.StartDate?.ToString("d"),
                startTime = courses.StartTime?.ToString("d"),
                endTime = courses.EndTime?.ToString("d"),
                capacity = courses.Capacity,
                monday = courses.Monday,
                tuesday = courses.Tuesday,
                wednesday = courses.Wednesday,
                thursday = courses.Thursday,
                friday = courses.Friday
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
                    InstructorId = user.Id,
                    Instructor = $"{user.LastName}, {user.FirstName}",
                    Code = model.code,
                    Name = model.name,
                    Description = model.description,
                    Department = model.department,
                    CreditHours = model.creditHours,
                    Location = model.location,
                    StartDate = model.startDate,
                    StartTime = DateTime.Parse((string)model.startTime),
                    EndTime = DateTime.Parse((string)model.endTime),
                    Capacity = model.capacity,
                    Monday = model.monday,
                    Tuesday = model.tuesday,
                    Wednesday = model.wednesday,
                    Thursday = model.thursday,
                    Friday = model.friday,
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

            if (user.Role && user.Id == course.InstructorId) {
                course.Code = model.code;
                course.Name = model.Name;
                course.Description = model.description;
                course.Department = model.department;
                course.CreditHours = model.creditHours;
                course.Location = model.location;
                course.StartDate = DateTime.Parse((string)model.startDate);
                course.StartTime = DateTime.Parse((string)model.startTime);
                course.EndTime = DateTime.Parse((string)model.endTime);
                course.Capacity = model.capacity;
                course.Monday = model.monday;
                course.Tuesday = model.tuesday;
                course.Wednesday = model.wednesday;
                course.Thursday = model.thursday;
                course.Friday = model.friday;

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
                    startDate = course.StartDate?.ToString("d"),
                    startTime = course.StartTime?.ToString("d"),
                    endTime = course.EndTime?.ToString("d"),
                    capacity = course.Capacity,
                    monday = course.Monday,
                    tuesday = course.Tuesday,
                    wednesday = course.Wednesday,
                    thursday = course.Thursday,
                    friday = course.Friday
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
                    course.StartDate,
                    course.StartTime,
                    course.EndTime,
                    course.Code,
                    course.Monday,
                    course.Tuesday,
                    course.Wednesday,
                    course.Thursday,
                    course.Friday
                });
            }

            return result;
        }
    }
}