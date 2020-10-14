using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public static partial class RepositoryExtensions {
        // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        public static async Task<List<Class>> GetClassesAsync(this IRepository<Class> repository) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Class>()
                    .ToListAsync();

            return result;
        }

        public static async Task<Class> GetClassByIdAsync(this IRepository<Class> repository, long id) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Class>()
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public static async Task<bool> ClassExistsByNameOrCodeAsync(this IRepository<Class> repository, string name, string code) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Class>()
                    .AnyAsync(x => x.Name == name || x.Code == code);

            return result;
        }
    }
}