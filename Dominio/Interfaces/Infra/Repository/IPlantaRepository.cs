using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces.Infra.Repository
{
    public interface IPlantaRepository
    {
        Task Inserir(Planta planta);
        Task InserirAtualizar(Expression<Func<Planta, bool>> condicao, Planta planta);
    }
}
