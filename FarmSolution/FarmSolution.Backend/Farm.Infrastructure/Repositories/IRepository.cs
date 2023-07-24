using Farm.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        FarmDbContext FarmDbContext { get; }
        IQueryable<TEntity> GetAll();
        ICollection<TEntity> GetList(Expression<Func<TEntity, bool>> filter);
        ICollection<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? page = null, int? pageSize = null);
        TEntity GetById(int id);
        TEntity GetByExternalId(Guid id);
        Task<TEntity> DeleteAsync(int id);
        TEntity Add(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}