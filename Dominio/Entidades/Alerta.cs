namespace Dominio.Entidades
{
    public class Alerta : EntidadeBase
    {
        public int Codigo { get; set; }
        public string ChavePs { get; set; }
        public int CodigoTipoFalha { get; set; }
        public string CodigoFalha { get; set; }
        public int TipoFalha { get; set; }
        public int NivelFalha { get; set; }
        public string DescricaoFalha { get; set; }
        public int StatusProcesso { get; set; }
        public string DataCriacao { get; set; }
        public long TempoProcesso { get; set; }
        public string MotivoFalha { get; set; }
        public string CodigoModeloDispositivo { get; set; }
        public string ModeloDispositivo { get; set; }
        public string NomeDispositivo { get; set; }
        public int TipoDispositivo { get; set; }
        public string NomePs { get; set; }
        public int Uuid { get; set; }
        public string NomeTipo { get; set; }
        public string NomeFalha { get; set; }
        public string TempoExcedido { get; set; }
    }
}
