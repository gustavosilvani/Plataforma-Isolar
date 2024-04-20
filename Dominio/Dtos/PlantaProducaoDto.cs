namespace Dominio.Dtos
{
    public class PlantaProducaoDto
    {
        public double PotenciaAtual { get; set; }
        public double RendaHoje { get; set; }
        public double EnergiaHoje { get; set; }
        public int StatusFalhaSistemaEnergia { get; set; }
        public int StatusOperacionalSistemaEnergia { get; set; }
        public string DataAtualizacaoEnergiaHoje { get; set; }
        public string DataAtualizacaoCapacidadeTotal { get; set; }
        public string DataAtualizacaoPotenciaAtual { get; set; }
        public DateTime DataCaptura { get; private set; }
    }
}
