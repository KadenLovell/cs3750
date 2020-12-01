using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<List<UserCourses>> GetUserCoursesById(this IRepository<UserCourses> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .Where(x => x.UserID == studentId)
                    .Include(x => x.Course)
                    .ToListAsync();

            return result;
        }

        public static async Task<UserCourses> GetUserCourseById(this IRepository<UserCourses> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .Include(x => x.Course)
                    .SingleOrDefaultAsync(x => x.Id == studentId);

            return result;
        }

        public static async Task<UserCourses> GetUserByEmailAsync(this IRepository<UserCourses> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .SingleOrDefaultAsync(x => x.Id == studentId);

            return result;
        }

        public static async Task<UserCourses> CheckDuplicateEntry(this IRepository<UserCourses> repository, long studentId, long courseId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .SingleOrDefaultAsync(x => x.UserID == studentId && x.CourseID == courseId);

            return result;
        }
    }
}