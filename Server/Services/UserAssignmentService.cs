using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class UserAssignmentService {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserAssignment> _repository;
        public UserAssignmentService(IHttpContextAccessor httpContextAccessor, IRepository<UserAssignment> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetUserAssignmentByCourseIdAsync(long courseId) {
            var userAssignment = await _repository.GetUserAssignmentByCourseId(courseId);
            
            return null;
        }

        public async Task<dynamic> AddUserAssignmentAsync(dynamic model) {
            var userAssignment = new UserAssignment {
                CourseId = model.courseId,
                UserId = model.userId,
                AssignmentId = model.assignmentId,
                FileContent = model.content,
                TextContent = model.textContent,
                ContentType = model.contentType
            };

            await _repository.AddAsync(userAssignment);

            var result = new {
                success = true,
                id = userAssignment.Id,
                courseId = userAssignment.CourseId,
                userId = userAssignment.UserId,
                assignmentId = userAssignment.AssignmentId,
                grade = "",
                fileContent = userAssignment.FileContent,
                textContent = userAssignment.TextContent
            };

            return null;
        }

        public async Task<dynamic> UpdateUserAssignmentAsync(dynamic model) {
            var id = (long) model.id;
            var userAssignment = await _repository.GetUserAssignmentById(id);

            userAssignment.FileContent = (byte[]) model.fileContent;
            userAssignment.TextContent = (string) model.textContent;

            await _repository.UpdateAsync(userAssignment);
            
            var result = new {
                success = true,
                id = userAssignment.Id,
                courseId = userAssignment.CourseId,
                userId = userAssignment.UserId,
                assignmentId = userAssignment.AssignmentId,
                grade = userAssignment.Assignment.Grade,
                assignmentName = userAssignment.Assignment.Title,
                fileContent = userAssignment.FileContent,
                textContent = userAssignment.TextContent
            };

            return result;
        }

        public async Task<dynamic> DeleteUserAssignmentsAsync(long courseId, long assignmentId) {
            var userAssignment = await _repository.GetUserAssignmentByCourseIdAndAssignmentId(courseId, assignmentId);
            
            await _repository.DeleteAsync(userAssignment);
            
            var result = new {
                success = true
            };

            return result;
        }

        public async Task<dynamic> UpdateUserAssignmentGradeAsync(dynamic model) {
            var id = (long) model.id;
            var userAssignment = await _repository.GetUserAssignmentById(id);

            userAssignment.Assignment.Grade = (double) model.grade;
            
            await _repository.UpdateAsync(userAssignment);

            var result = new {
                success = true,
                id = userAssignment.Id,
                courseId = userAssignment.CourseId,
                userId = userAssignment.UserId,
                assignmentId = userAssignment.AssignmentId,
                grade = userAssignment.Assignment.Grade,
                assignmentName = userAssignment.Assignment.Title,
                fileContent = userAssignment.FileContent,
                textContent = userAssignment.TextContent
            };

            return result;
        }
    }
}
