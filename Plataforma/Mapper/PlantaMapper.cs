using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;

namespace Plataforma.Mapper
{
    public class PlantaMapper : Profile
    {
        public PlantaMapper()
        {
            CreateMap<Planta, PlantaDto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src._id))
                .ReverseMap();
        }
    }
}