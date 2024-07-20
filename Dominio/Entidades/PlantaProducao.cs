using Dominio.Enumeradores;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dominio.Entidades
{
    public class PlantaProducao : EntidadeBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _idPlanta { get; private set; }
        public double PotenciaAtual { get; private set; }
        public double RendaHoje { get; private set; }
        public double EnergiaHoje { get; private set; }
        public int StatusFalhaSistemaEnergia { get; private set; }
        public int StatusOperacionalSistemaEnergia { get; private set; }
        public string DataAtualizacaoEnergiaHoje { get; private set; }
        public string DataAtualizacaoCapacidadeTotal { get; private set; }
        public string DataAtualizacaoPotenciaAtual { get; private set; }
        public DateTime DataCaptura { get; private set; }
        public TipoIntegracao TipoIntegracao { get; private set; }

        public void DefinirIdPlanta(string idPlanta)
        {
            _idPlanta = idPlanta;
        }

        public void DefinirDataCaptura()
        {
            DataCaptura = DateTime.Now;
        }
    }
}
