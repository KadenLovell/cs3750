using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class AssignmentService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Assignment> _repository;

        public AssignmentService(IHttpContextAccessor httpContextAccessor, IRepository<Assignment> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetAssignmentsAsync(long courseId) {
            var assignments = await _repository.GetAssignmentsAsync(courseId);
            if (assignments == null || assignments.Count == 0) {
                return null;
            }

            var result = new List<dynamic>();

            foreach (var assignment in assignments) {
                result.Add(new {
                    assignment.Id,
                    assignment.Title,
                    assignment.CreatedDate,
                    assignment.DueDate,
                    assignment.DueTime,
                    assignment.MaxPoints,
                    submissionType = assignment.AssignmentType,
                    assignment.CourseId
                });
            }

            return result;
        }

        public async Task<dynamic> GetAssignmentAsync(long id) {
            var assignment = await _repository.GetAssignmentById(id);

            var result = new {
                id = assignment.Id,
                title = assignment.Title,
                createdDate = assignment.CreatedDate,
                dueDate = assignment.DueDate,
                dueTime = assignment.DueTime,
                maxPoints = assignment.MaxPoints,
                courseId = assignment.CourseId
            };

            return result;
        }

        public async Task<dynamic> AddAssignmentAsync(dynamic model) {
            var assignment = new Assignment {
                Title = model.title,
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.Parse((string)model.dueDate),
                DueTime = DateTime.Parse((string)model.dueTime).TimeOfDay,
                MaxPoints = model.maxPoints,
                AssignmentType = model.submissionType,
                CourseId = model.courseId
            };

            await _repository.AddAsync(assignment);

            var result = new {
                success = true,
                assignment = new {
                    id = assignment.Id,
                    title = assignment.Title,
                    createdDate = assignment.CreatedDate,
                    dueDate = assignment.DueDate,
                    dueTime = assignment.DueTime,
                    maxPoints = assignment.MaxPoints,
                    submissionType = assignment.AssignmentType,
                    courseId = assignment.CourseId
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateAssignmentAsync(dynamic model) {
            var assignment = await _repository.GetAssignmentById((long)model.id);

            if (assignment == null) {
                return null;
            }

            assignment.Title = model.title;
            assignment.CreatedDate = model.createdDate;
            assignment.DueDate = model.duedate;
            assignment.DueTime = model.dueTime;
            assignment.MaxPoints = model.maxPoints;
            assignment.CourseId = model.courseId;

            await _repository.UpdateAsync(assignment);

            var result = new {
                success = true,
                id = assignment.Id,
                title = assignment.Title,
                createdDate = assignment.CreatedDate,
                dueDate = assignment.DueDate,
                dueTime = assignment.DueTime,
                maxPoints = assignment.MaxPoints,
                courseId = assignment.CourseId
            };

            return result;
        }
    }
}