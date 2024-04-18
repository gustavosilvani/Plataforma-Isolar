namespace Dominio.Entidades
{
    public class Planta : EntidadeBase
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        public double EnergiaTotal { get; private set; }
        public string Localizacao { get; private set; }
        public bool StatusConexaoRede { get; private set; }
        public double RendaTotal { get; private set; }
        public double CapacidadeTotal { get; private set; }
        public string HoraEquivalente { get; private set; }
        public double ReducaoTotalCO2 { get; private set; }
        public int ContagemFalhas { get; private set; }
    }
}
