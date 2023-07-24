using Farm.Service.DTO;
using Farm.Service.DTO.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Service.Abstract
{
    public interface IAnimalService
    {
        ServiceResultDto Create(AnimalDto request);
        IEnumerable<AnimalDto> List();
        AnimalDto GetByExternalId(Guid id);
        Task<ServiceResultDto> DeleteAsync(Guid id);
        Task<ServiceResultDto> Update(AnimalDto request);

    }
}
