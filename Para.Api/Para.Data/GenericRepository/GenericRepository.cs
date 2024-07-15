using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Context;

namespace Para.Data.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ParaDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ParaDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetById(long Id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Insert(TEntity entity)
        {
            entity.IsActive = true;
            entity.InsertDate = DateTime.UtcNow;
            entity.InsertUser = "System";
            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task Delete(long Id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == Id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }
    }
}
