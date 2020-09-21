using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IModel {
        private readonly IPersistenceContext _persistenceContext;
        private readonly DataContext _context;
        private DbSet<TModel> entities;
        public Repository(IPersistenceContext persistenceContext, DataContext context) {
            _context = context;
            _persistenceContext = persistenceContext;
            entities = context.Set<TModel>();
        }

        public IQueryable AsQueryable() {
            return _persistenceContext.Set<TModel>();
        }

        public async Task AddAsync(TModel model) {
            if (model == null) throw new ArgumentNullException("entity");

            entities.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TModel model) {
            if (model == null) throw new ArgumentNullException("entity");
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id) {
            TModel entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            entities.Remove(entity);
            _context.SaveChanges();

        }
    }
}