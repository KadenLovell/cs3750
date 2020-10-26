using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<List<Course>> GetCoursesAsync(this IRepository<Course> repository) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Course>()
                    .ToListAsync();

            return result;
        }

        public static async Task<Course> GetCourseByIdAsync(this IRepository<Course> repository, long id) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Course>()
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public static async Task<bool> CourseExistsByNameOrCodeAsync(this IRepository<Course> repository, string name, string code) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Course>()
                    .AnyAsync(x => x.Name == name || x.Code == code);

            return result;
        }


        public static async Task<List<Course>> SearchCourses(this IRepository<Course> repository, string name, string department, string instructor) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Course>()
                    .Where(x => x.Name.Contains(name ?? ""))
                    .Where(x => x.Department.Contains(department ?? ""))
                    .Where(x => x.Instructor.Contains(instructor ?? ""))
                    .ToListAsync();

            return result;
        }
    }
}