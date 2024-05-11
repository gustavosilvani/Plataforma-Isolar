using Dominio.Dtos;

namespace Dominio.Interfaces.Services
{
    public interface IPlantaService
    {
        Task<List<PlantaDto>> ObterTodos();
        Task<List<PlantaProducaoCompletaDto>> ObterTodosComProducao();
    }
}
