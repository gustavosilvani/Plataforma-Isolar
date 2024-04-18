using Dominio.Entidades;
using Dominio.Interfaces.Infra.Data;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Data.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntidadeBase
    {
        private readonly IMongoCollection<T> _dbContext;
        public GenericRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext.ObterCollection().GetCollection<T>(typeof(T).Name);
        }

        #region Read

        public async Task<List<T>> GetAsync() =>
               await _dbContext.Find(_ => true).ToListAsync();

        public async Task<List<string>> GetDistinctAsync(FieldDefinition<T, string> campo, FilterDefinition<T> filtro) =>
               await _dbContext.Distinct(campo, filtro).ToListAsync();

        public async Task<List<T>> GetByExpressionAsync(Expression<Func<T, bool>> condicao) =>
        await _dbContext.Find(condicao).ToListAsync();

        public async Task<T> GetOneByExpressionAsync(Expression<Func<T, bool>> condicao) =>
        await _dbContext.Find(condicao).FirstOrDefaultAsync();

        #endregion

        #region Create

        public async Task CreateOneAsync(T entity) =>
            await _dbContext.InsertOneAsync(entity);

        public async Task CreateManyAsync(List<T> entity) =>
            await _dbContext.InsertManyAsync(entity);

        public async Task<List<T>> CreateManyRetunAsync(List<T> entity)
        {
            await _dbContext.InsertManyAsync(entity);
            return entity;
        }

        public async Task<T> CreateOrUpdateAsync(Expression<Func<T, bool>> condicao, T entity)
        {
            var updateOptions = new FindOneAndUpdateOptions<T, T>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };

            var update = Builders<T>.Update;
            var updateDefinition = new List<UpdateDefinition<T>>();

            foreach (var prop in entity.GetType().GetProperties())
            {
                var value = prop.GetValue(entity);
                var field = prop.Name;
                updateDefinition.Add(update.SetOnInsert(field, value));
            }

            var combinedUpdateDefinition = update.Combine(updateDefinition);

            return await _dbContext.FindOneAndUpdateAsync(condicao, combinedUpdateDefinition, updateOptions);
        }

        #endregion

        #region Update

        public async Task FindOneAndUpdateAsync(FilterDefinition<T> filtro, UpdateDefinition<T> entity) =>
            await _dbContext.FindOneAndUpdateAsync(filtro, entity);

        #endregion

        #region Delete

        public async Task DeleteAsync(FilterDefinition<T> filtro) =>
            await _dbContext.DeleteManyAsync(filtro);

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filtro) =>
            await _dbContext.DeleteOneAsync(filtro);

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filtro) =>
            await _dbContext.DeleteManyAsync(filtro);

        #endregion
    }

}
