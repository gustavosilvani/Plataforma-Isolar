using AutoMapper;
using Dominio.Dtos;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;

namespace Service.Services
{
    public class PlantaService : IPlantaService
    {
        private readonly IPlantaRepository _plantaRepository;
        private readonly IMapper _mapper;

        public PlantaService(IPlantaRepository plantaRepository, IMapper mapper)
        {
            _plantaRepository = plantaRepository;
            _mapper = mapper;
        }

        public async Task<List<PlantaDto>> ObterTodos()
        {
            var plantas = await _plantaRepository.ObterTodos();

            return _mapper.Map<List<PlantaDto>>(plantas);
        }

    }
}
