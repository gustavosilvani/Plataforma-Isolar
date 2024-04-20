using Dominio.Entidades;

namespace Dominio.Interfaces.Infra.Repository
{
    public interface IPlantaProducaoRepository
    {
        Task<List<PlantaProducao>> ObterPorPlanta(string idPlanta);
        Task Inserir(PlantaProducao planta);
    }
}
