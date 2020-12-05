using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<List<UserAssignment>> GetUserAssignments(this IRepository<UserAssignment> repository, long courseId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserAssignment>()
                    .Where(x => x.CourseId == courseId)
                    .Include(x => x.Assignment)
                    .ToListAsync();

            return result;
        }
        public static async Task<UserAssignment> GetUserAssignmentById(this IRepository<UserAssignment> repository, long id) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserAssignment>()
                    .Include(x => x.Assignment)
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public static async Task<UserAssignment> GetUserAssignmentByCourseId(this IRepository<UserAssignment> repository, long courseId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserAssignment>()
                    .Include(x => x.Assignment)
                    .SingleOrDefaultAsync(x => x.CourseId == courseId);

            return result;
        }

        public static async Task<UserAssignment> GetUserAssignmentByCourseIdAndAssignmentId(this IRepository<UserAssignment> repository, long courseId, long assignmentId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<UserAssignment>()
                    .Include(x => x.Assignment)
                    .SingleOrDefaultAsync(x => x.CourseId == courseId && x.AssignmentId == assignmentId);

            return result;
        }
    }
}