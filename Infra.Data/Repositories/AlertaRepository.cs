using Dominio.Entidades;
using Dominio.Interfaces.Infra.Data;
using Dominio.Interfaces.Infra.Repository;
using Infra.Data.Data;
using System.Linq.Expressions;

namespace Infra.Data.Repositories
{
    public class AlertaRepository : GenericRepository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(IMongoDbContext dbContext) : base(dbContext) { }


        public async Task<List<Alerta>> ObterTodos() =>
            await GetAsync();

        public async Task Inserir(Alerta alerta) =>
            await CreateOneAsync(alerta);

        public async Task<Alerta> InserirAtualizar(Expression<Func<Alerta, bool>> condicao, Alerta alerta) =>
            await CreateOrUpdateAsync(condicao, alerta);
    }
}