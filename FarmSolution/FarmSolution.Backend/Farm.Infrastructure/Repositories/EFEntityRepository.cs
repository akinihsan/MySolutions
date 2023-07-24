using Farm.Core.Entities.Base;
using Farm.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Infrastructure.Repositories
{
    public class EFEntityRepository<TEntity> : EFRepository<TEntity> where TEntity : EntityBase, new()
    {
        public EFEntityRepository(FarmDbContext FarmDbContext) : base(FarmDbContext)
        {
        }

        public override TEntity GetByExternalId(Guid id)
        {
            return this.FarmDbContext.Set<TEntity>().SingleOrDefault(w => w.ExternalId == id);
        }
    }
}
