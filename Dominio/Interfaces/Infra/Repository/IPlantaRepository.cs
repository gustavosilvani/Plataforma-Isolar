using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces.Infra.Repository
{
    public interface IPlantaRepository
    {
        Task<List<Planta>> ObterTodos();
        Task Inserir(Planta planta);
        Task<Planta> InserirAtualizar(Expression<Func<Planta, bool>> condicao, Planta planta);
    }
}
