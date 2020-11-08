using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence {
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IModel {
        private readonly IPersistenceContext _persistenceContext;
        private readonly DataContext _dataContext;
        private DbSet<TModel> entities;
        public Repository(IPersistenceContext persistenceContext, DataContext dataContext) {
            _dataContext = dataContext;
            _persistenceContext = persistenceContext;
            entities = dataContext.Set<TModel>();
        }

        public IQueryable AsQueryable() {
            return _persistenceContext.Set<TModel>();
        }

        public async Task AddAsync(TModel model) {
            if (model == null) throw new ArgumentNullException("entity");
            entities.Add(model);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TModel model) {
            if (model == null) throw new ArgumentNullException("entity");
            entities.Update(model);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id) {
            TModel entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            entities.Remove(entity);
            _dataContext.SaveChanges();
        }
        // public void test(){
        //     _dataContext.User.

        // }    
}
}