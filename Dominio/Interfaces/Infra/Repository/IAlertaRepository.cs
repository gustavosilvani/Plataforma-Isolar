using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces.Infra.Repository
{
    public interface IAlertaRepository
    {
        Task<List<Alerta>> ObterTodos();
        Task Inserir(Alerta alerta);
        Task<Alerta> InserirAtualizar(Expression<Func<Alerta, bool>> condicao, Alerta alerta);
    }
}
