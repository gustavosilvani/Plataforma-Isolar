using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;

namespace Jobs.Mapper
{
    public class PlantaJobMapper : Profile
    {
        public PlantaJobMapper()
        {
            CreateMap<Planta, SungrowRetornoPlantaListaDto>()
                .ForMember(dest => dest.NomeSistemaEnergia, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.IdSistemaEnergia, opt => opt.MapFrom(src => src.Codigo))
                .ForPath(dest => dest.EnergiaTotal.Valor, opt => opt.MapFrom(src => src.EnergiaTotal))
                .ForMember(dest => dest.LocalizacaoSistemaEnergia, opt => opt.MapFrom(src => src.Localizacao))
                .ForMember(dest => dest.StatusConexaoRede, opt => opt.MapFrom(src => src.StatusConexaoRede))
                .ForPath(dest => dest.RendaTotal.Valor, opt => opt.MapFrom(src => src.RendaTotal))
                .ForPath(dest => dest.CapacidadeTotal.Valor, opt => opt.MapFrom(src => src.CapacidadeTotal))
                .ForPath(dest => dest.HoraEquivalente.Valor, opt => opt.MapFrom(src => src.HoraEquivalente))
                .ForPath(dest => dest.ReducaoTotalCO2.Valor, opt => opt.MapFrom(src => src.ReducaoTotalCO2))
                .ForMember(dest => dest.ContagemFalhas, opt => opt.MapFrom(src => src.ContagemFalhas))
                .ForMember(dest => dest.TipoIntegracao, opt => opt.MapFrom(src => src.TipoIntegracao))
                .ReverseMap();
        }
    }
}
