using Dominio.Entidades;
using Dominio.Interfaces.Infra.Data;
using Dominio.Interfaces.Infra.Repository;
using Infra.Data.Data;

namespace Infra.Data.Repositories
{
    public class PlantaProducaoRepository : GenericRepository<PlantaProducao>, IPlantaProducaoRepository
    {
        public PlantaProducaoRepository(IMongoDbContext dbContext) : base(dbContext) { }

        public async Task<List<PlantaProducao>> ObterPorPlanta(string idPlanta) =>
            await GetByExpressionAsync(x => x._idPlanta == idPlanta);

        public async Task Inserir(PlantaProducao planta) =>
            await CreateOneAsync(planta);

    }
}
