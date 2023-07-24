using AutoMapper;
using Farm.Service.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farm.Infrastructure.Repositories.Abstract;
using Farm.Infrastructure.Repositories.Concrete;
using Farm.Service.DTO.Animal;
using Farm.Core.Entities;
using Farm.Service.DTO;

namespace Farm.Service.Concrete
{
    public class AnimalService : IAnimalService
    {
        private IMapper mapper;
        private ILogger<AnimalService> logger;
        private readonly IAnimalRepository animalRepository;
        public AnimalService(IMapper mapper, ILogger<AnimalService> logger, IAnimalRepository animalRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.animalRepository = animalRepository;
        }
        public ServiceResultDto Create(AnimalDto request)
        {
            Animal animal = this.mapper.Map<Animal>(request);

            // check same animal exist 
            var sameAnimalExists = this.animalRepository.GetAll().Where(a => a.Name == request.Name).Any();

            if (sameAnimalExists)
            {
                return new ServiceResultDto(false, "Same animal already exists");
            }

            animal = this.animalRepository.Add(animal);

            return new ServiceResultDto(true, "", animal.ExternalId.ToString());
        }
        public AnimalDto GetByExternalId(Guid id)
        {
            return mapper.Map<AnimalDto>(this.animalRepository.GetByExternalId(id));
        }
        public async Task<ServiceResultDto> DeleteAsync(Guid id)
        {
            var animal = this.animalRepository.GetByExternalId(id);
            if (animal == null)
            {
                return new ServiceResultDto(false, "Animal does not exist");
            }
            await this.animalRepository.DeleteAsync(animal.Id);

            return new ServiceResultDto(true);
        }
        public async Task<ServiceResultDto> Update(AnimalDto request)
        {
            var sameNameExists = this.animalRepository.GetAll().Where(a => a.Name == request.Name && a.ExternalId != request.ExternalId).Any();
            if (sameNameExists)
            {
                return new ServiceResultDto(false, "This name belongs to other animal");
            }

            var animal = this.animalRepository.GetByExternalId(request.ExternalId);

            if (animal == null)
            {
                return new ServiceResultDto(false, "Animal not found");
            }

            animal.Name = request.Name;

            await this.animalRepository.UpdateAsync(animal);
            return new ServiceResultDto(true);
        }
        public IEnumerable<AnimalDto> List()
        {
            var list = this.animalRepository.GetAll();

            return mapper.Map<IEnumerable<AnimalDto>>(list);
        }
    }
}
