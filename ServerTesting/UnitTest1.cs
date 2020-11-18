using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Controllers;
using System;
using Server.Persistence;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace ServerTesting
{
    [TestClass]
    public class UnitTest1
    {
        public DataContext _context { get; private set; }

        [TestMethod]
        public async Task AssignmentTestAsync()
        {
            DbContextOptions<DataContext> options = new DbContextOptions<DataContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=localhost;Initial Catalog=LMS-sandbox;Integrated Security=True;MultipleActiveResultSets=True;", null);
            _context = new DataContext((DbContextOptions<DataContext>)builder.Options);
            var assignments = await _context.Assignment.ToListAsync();
            var courses = await _context.Course.ToListAsync();
            long courseId = 0;
            if (courses.Count > 0)
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    var currentNumAssignments = 0;
                    var newNumAssignments = 0;
                    courseId = courses[i].Id;
                    for (int j = 0; j < assignments.Count; j++) {
                        if (assignments[j].CourseId == courseId) {
                            currentNumAssignments++;
                        }
                    }
                    Assignment assignment = new Assignment();
                    assignment.Title = "Test Assignment";
                    assignment.DueDate = DateTime.Now;
                    assignment.DueTime = null;
                    assignment.CourseId = courseId;
                    assignment.AssignmentType = 0;
                    assignment.MaxPoints = 100;
                    assignment.CreatedDate = DateTime.Now;
                    _context.Assignment.Add(assignment);
                    _context.SaveChanges();
                    assignments = await _context.Assignment.ToListAsync();
                    courses = await _context.Course.ToListAsync();
                    for (int k = 0; k < assignments.Count; k++)
                    {
                        if (assignments[k].CourseId == courseId)
                        {
                            newNumAssignments++;
                        }
                    }
                    Assert.AreEqual(newNumAssignments, currentNumAssignments + 1);
                }
            }
        }

        [TestMethod]
        public async Task CourseAddTestAsync()
        {
            DbContextOptions<DataContext> options = new DbContextOptions<DataContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=localhost;Initial Catalog=LMS-sandbox;Integrated Security=True;MultipleActiveResultSets=True;", null);
            _context = new DataContext((DbContextOptions<DataContext>)builder.Options);

            var courses = await _context.Course.ToListAsync();
            var users = await _context.User.ToListAsync();

            long temp = 0;
            for (int i = 0; i < users.Count; i++) { // loop through the users and find an admin
                if (users[i].Role == 1) {
                    temp = users[i].Id;
                }
            }
            if (temp == 0) {
                // create a new user of type instructor
            }

            int oldInstructorCourseCount = 0;
            int newInstructorCourseCount = 0;

            for (int i = 0; i<courses.Count; i++) {
                if (courses.InstructorId == temp) { oldInstructorCourseCount++; }
            }

            Course course = new Course();
            course.InstructorId = temp;     //assigns the new course to an instructor
            course.Code = "test";
            course.Name = "test";
            course.Instructor = "test, test";
            course.Description = "test";
            course.Department = "Computer Science";
            course.CreditHours = 20;
            course.Location = "Main Campus";
            course.StartDate = new DateTime();
            course.StartTime = new DateTime();
            course.EndTime = new DateTime();
            course.Capacity = 20;
            course.Monday = 1;
            course.Tuesday = 1;
            course.Wednesday = 1;
            course.Thursday = 1;
            course.Friday = 1;
            _context.Course.Add(course);
            _context.SaveChanges();

            var courses = await _context.Course.ToListAsync();
            for (int i = 0; i<courses.Count; i++) {
                if (courses.InstructorId == temp) { newInstructorCourseCount++; }
            }

            Assert.AreEqual(oldInstructorCourseCount+1, newInstructorCourseCount);
        }

    }
}
