using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<UserCourse> GetUserCourseById(this IRepository<UserCourse> repository, long studentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<User>()
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

                //searching based on email
        public static async Task<UserCourse> GetUserByEmailAsync(this IRepository<UserCourse> repository, string email) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserCourse>()
                    .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return result;
        }

        public static async Task<bool> UserExistsByUsernameOrEmail(this IRepository<User> repository, string username, string email) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<User>()
                    .AnyAsync(x => x.Username == username || x.Email.ToLower() == email.ToLower());

            return result;
        }
    }
}