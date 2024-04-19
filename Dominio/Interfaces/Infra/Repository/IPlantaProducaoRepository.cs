using Dominio.Entidades;

namespace Dominio.Interfaces.Infra.Repository
{
    public interface IPlantaProducaoRepository
    {
        Task Inserir(PlantaProducao planta);
    }
}
