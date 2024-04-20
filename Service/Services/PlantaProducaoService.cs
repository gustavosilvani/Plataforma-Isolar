using AutoMapper;
using Dominio.Dtos;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;

namespace Service.Services
{
    public class PlantaProducaoService : IPlantaProducaoService
    {
        private readonly IPlantaProducaoRepository _plantaProducaoRepository;
        private readonly IMapper _mapper;
        public PlantaProducaoService(IPlantaProducaoRepository plantaProducaoRepository, IMapper mapper)
        {
            _plantaProducaoRepository = plantaProducaoRepository;
            _mapper = mapper;
        }

        public async Task<List<PlantaProducaoDto>> ObterPorPlanta(string codigo)
        {
            var plantasProducao = await _plantaProducaoRepository.ObterPorPlanta(codigo);

            return _mapper.Map<List<PlantaProducaoDto>>(plantasProducao);
        }
    }
}
