using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dominio.Entidades
{
    public class EntidadeBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }
    }
}