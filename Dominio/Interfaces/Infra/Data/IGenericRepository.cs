using Dominio.Entidades;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Dominio.Interfaces.Infra.Data
{
    public interface IGenericRepository<T> where T : EntidadeBase
    {
        Task<List<T>> GetAsync();
        Task<List<string>> GetDistinctAsync(FieldDefinition<T, string> campo, FilterDefinition<T> filtro);
        Task<List<T>> GetByExpressionAsync(Expression<Func<T, bool>> condicao);
        Task<T> GetOneByExpressionAsync(Expression<Func<T, bool>> condicao);
        Task CreateOneAsync(T dto);
        Task CreateManyAsync(List<T> dto);
        Task<List<T>> CreateManyRetunAsync(List<T> dto);
        Task DeleteAsync(FilterDefinition<T> filtro);
        Task DeleteOneAsync(Expression<Func<T, bool>> filtro);
        Task DeleteManyAsync(Expression<Func<T, bool>> filtro);
        Task FindOneAndUpdateAsync(FilterDefinition<T> filtro, UpdateDefinition<T> dto);
    }
}