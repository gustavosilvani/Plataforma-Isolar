using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;

namespace Plataforma.Mapper
{
    public class AlertaMapper : Profile
    {
        public AlertaMapper()
        {
            CreateMap<Alerta, SungrowRetornoAlertaPlantaDto>()
                .ForMember(dest => dest.IdPs, opt => opt.MapFrom(src => src.Codigo))
                .ReverseMap();
        }
    }
}