namespace Dominio.Dtos
{
    public class PlantaProducaoCompletaDto
    {
        public virtual PlantaDto Planta { get; set; }
        public virtual List<PlantaProducaoDto> Producao { get; set; }
    }
}
