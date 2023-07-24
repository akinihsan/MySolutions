using AutoMapper;
using Farm.Core.Entities;
using Farm.Service.DTO.Animal;
using System.Net;

namespace Farm.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Animal, AnimalDto>().ReverseMap();
        }
    }
}
