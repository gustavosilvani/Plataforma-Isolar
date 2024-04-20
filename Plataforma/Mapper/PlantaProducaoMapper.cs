using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;

namespace Plataforma.Mapper
{
    public class PlantaProducaoMapper : Profile
    {
        public PlantaProducaoMapper()
        {
            CreateMap<PlantaProducaoDto, PlantaProducao>().ReverseMap();
        }
    }
}