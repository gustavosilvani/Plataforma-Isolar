using Dominio.Entidades;

namespace Dominio.Interfaces.Services
{
    public interface IAlertaService
    {
        Task<List<Alerta>> ObterTodos();

    }
}
