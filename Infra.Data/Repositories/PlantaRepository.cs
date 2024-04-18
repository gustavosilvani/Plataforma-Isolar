using Dominio.Entidades;
using Dominio.Interfaces.Infra.Data;
using Dominio.Interfaces.Infra.Repository;
using Infra.Data.Data;
using System.Linq.Expressions;

namespace Infra.Data.Repositories
{
    public class PlantaRepository : GenericRepository<Planta>, IPlantaRepository
    {
        public PlantaRepository(IMongoDbContext dbContext) : base(dbContext) { }

        public async Task Inserir(Planta planta) =>
            await CreateOneAsync(planta);
        public async Task InserirAtualizar(Expression<Func<Planta, bool>> condicao, Planta planta) =>
            await CreateOrUpdateAsync(condicao, planta);
    }
}
