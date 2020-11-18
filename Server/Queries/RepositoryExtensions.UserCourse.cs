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
                    .Where(x => x.Id == studentId)
                    .ToListAsync();

            return result;
        }

        public static async Task<UserCourses> GetUserCourseById(this IRepository<UserCourses> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .SingleOrDefaultAsync(x => x.Id == studentId);

            return result;
        }

        //searching based on email
        public static async Task<UserCourses> GetUserByEmailAsync(this IRepository<UserCourses> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .SingleOrDefaultAsync(x => x.Id == studentId);

            return result;
        }

        public static async Task<UserCourses> CheckDuplicateEntry(this IRepository<UserCourses> repository, long studentId, string courseId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourses>()
                    .SingleOrDefaultAsync(x => x.UserID == studentId && x.CourseID == courseId);

            return result;

        }



        // public static async Task<bool> UserExistsByUsernameOrEmail(this IRepository<User> repository, string username, string email) {
        //     var result =
        //         await repository
        //             .AsQueryable()
        //             .OfType<User>()
        //             .AnyAsync(x => x.Username == username || x.Email.ToLower() == email.ToLower());

        //     return result;
        // }
    }
}