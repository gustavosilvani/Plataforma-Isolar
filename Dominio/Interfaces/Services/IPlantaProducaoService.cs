using Dominio.Dtos;

namespace Dominio.Interfaces.Services
{
    public interface IPlantaProducaoService
    {
        Task<List<PlantaProducaoDto>> ObterPorPlanta(string idPlanta);
    }
}
