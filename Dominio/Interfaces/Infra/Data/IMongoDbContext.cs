using MongoDB.Driver;

namespace Dominio.Interfaces.Infra.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase ObterCollection();
    }
}
