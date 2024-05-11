using AutoMapper;
using Dominio.Dtos;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;

namespace Service.Services
{
    public class PlantaService : IPlantaService
    {
        private readonly IPlantaRepository _plantaRepository;
        private readonly IPlantaProducaoService _plantaProducaoService;
        private readonly IMapper _mapper;

        public PlantaService(IPlantaRepository plantaRepository, IPlantaProducaoService plantaProducaoService, IMapper mapper)
        {
            _plantaRepository = plantaRepository;
            _plantaProducaoService = plantaProducaoService;
            _mapper = mapper;
        }

        public async Task<List<PlantaDto>> ObterTodos()
        {
            var plantas = await _plantaRepository.ObterTodos();

            return _mapper.Map<List<PlantaDto>>(plantas);
        }

        public async Task<List<PlantaProducaoCompletaDto>> ObterTodosComProducao()
        {
            var plantas = await _plantaRepository.ObterTodos();

            var plantasProducao = new List<PlantaProducaoCompletaDto>();

            foreach (var item in plantas)
            {
                plantasProducao.Add(new PlantaProducaoCompletaDto
                {
                    Planta = _mapper.Map<PlantaDto>(item),
                    Producao = _mapper.Map<List<PlantaProducaoDto>>(await _plantaProducaoService.ObterPorPlanta(item._id))
                });
            }

            return plantasProducao;
        }

    }
}
