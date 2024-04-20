namespace Dominio.Dtos
{
    public class PlantaDto
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double EnergiaTotal { get; set; }
        public string Localizacao { get; set; }
        public bool StatusConexaoRede { get; set; }
        public double RendaTotal { get; set; }
        public double CapacidadeTotal { get; set; }
        public string HoraEquivalente { get; private set; }
        public double ReducaoTotalCO2 { get; set; }
        public int ContagemFalhas { get; set; }
    }
}
