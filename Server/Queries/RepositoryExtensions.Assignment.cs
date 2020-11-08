using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<List<Assignment>> GetAssignmentsAsync(this IRepository<Assignment> repository, long courseId) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Assignment>()
                    .Where(x => x.CourseId == courseId)
                    .ToListAsync();

            return result;
        }

        public static async Task<Assignment> GetAssignmentById(this IRepository<Assignment> repository, long id) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Assignment>()
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}