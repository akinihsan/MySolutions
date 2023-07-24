using Farm.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Infrastructure.Repositories
{

    public abstract class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public FarmDbContext FarmDbContext { get; }

        public EFRepository(FarmDbContext FarmDbContext) => this.FarmDbContext = FarmDbContext;

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return this.FarmDbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public ICollection<TEntity> GetList(Expression<Func<TEntity, bool>> filter) => filter == null
                ? this.FarmDbContext.Set<TEntity>().ToList()
                : this.FarmDbContext.Set<TEntity>().Where(filter).ToList();

        public ICollection<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? page = null, int? pageSize = null)
        {

            IQueryable<TEntity> query = this.FarmDbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{typeof(TEntity)} entity must not be null");
            }

            try
            {
                this.FarmDbContext.Add(entity);
                this.FarmDbContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{typeof(TEntity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await this.FarmDbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            this.FarmDbContext.Set<TEntity>().Remove(entity);
            await this.FarmDbContext.SaveChangesAsync();
            return entity;
        }

        public TEntity GetById(int id) => FarmDbContext.Set<TEntity>().Find(id);

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{typeof(TEntity)} entity must not be null");
            }
            try
            {
                this.FarmDbContext.Update(entity);
                await this.FarmDbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{typeof(TEntity)} could not be updated: {ex.Message}");
            }
        }

        public abstract TEntity GetByExternalId(Guid id);
    }
}
