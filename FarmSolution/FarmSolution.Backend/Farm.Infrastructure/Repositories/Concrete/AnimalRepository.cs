using Farm.Core.Entities;
using Farm.Infrastructure.DBContext;
using Farm.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Infrastructure.Repositories.Concrete
{ 
    public class AnimalRepository : EFEntityRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(FarmDbContext FarmDbContext) : base(FarmDbContext)
        {
        }
    }
}
