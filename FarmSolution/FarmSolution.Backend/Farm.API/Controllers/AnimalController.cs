using AutoMapper;
using Farm.API.Model.Base;
using Farm.Core.Entities;
using Farm.Service.Abstract;
using Farm.Service.DTO.Animal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farm.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnimalController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<AnimalController> logger;
        private readonly IAnimalService animalService;
        public AnimalController(IMapper mapper, ILogger<AnimalController> logger, IAnimalService animalService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.animalService = animalService;
        }


        [HttpGet("[action]")]
        public ApiResult<IEnumerable<AnimalDto>> Get()
        {
            IEnumerable<AnimalDto> result = animalService.List();
            return new ApiResult<IEnumerable<AnimalDto>>(true, string.Empty, result);
        }


        [HttpGet("[action]")]
        public ApiResult<AnimalDto> GetById([FromQuery()] Guid animalId)
        {
            AnimalDto animal = animalService.GetByExternalId(animalId);
            return new ApiResult<AnimalDto>(true, null, animal);
        }

        [HttpPost("[action]")]
        public ApiResult<string> Post([FromBody] string name)
        {
            var result = this.animalService.Create(new AnimalDto() { Name = name });

            if (result.Successful)
                return new ApiResult<string>(true, string.Empty, result.ResultId);
            else
                return new ApiResult<string>(false, result.ErrorMessage, string.Empty);
        }

        [HttpPut("[action]")]
        public async Task<ApiResult<string>> Put([FromBody] AnimalDto animalDto)
        {
            var result = await this.animalService.Update(animalDto);

            if (result.Successful)
                return new ApiResult<string>(true, string.Empty, result.ResultId);
            else
                return new ApiResult<string>(false, result.ErrorMessage, string.Empty);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<ApiResult<string>> DeleteAsync(Guid id)
        {
            var result = await this.animalService.DeleteAsync(id);

            if (result.Successful)
                return new ApiResult<string>(true, string.Empty, result.ResultId);
            else
                return new ApiResult<string>(false, result.ErrorMessage, string.Empty);
        }
    }
}
