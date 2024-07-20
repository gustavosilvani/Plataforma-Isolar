using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;
using Infra.CrossCutting.Helpers;

namespace Jobs.Mapper
{
    public class PlantaProducaoJobMapper : Profile
    {
        public PlantaProducaoJobMapper()
        {
            CreateMap<SungrowRetornoPlantaListaDto, PlantaProducao>()
                .ForMember(dest => dest.PotenciaAtual, opt => opt.MapFrom(src => ConversaoHelpper.ParaDouble(src.PotenciaAtual.Valor)))
                .ForMember(dest => dest.RendaHoje, opt => opt.MapFrom(src => ConversaoHelpper.ParaDouble(src.RendaHoje.Valor)))
                .ForMember(dest => dest.EnergiaHoje, opt => opt.MapFrom(src => ConversaoHelpper.ParaDouble(src.EnergiaHoje.Valor)))
                .ForMember(dest => dest.StatusFalhaSistemaEnergia, opt => opt.MapFrom(src => src.StatusFalhaSistemaEnergia))
                .ForMember(dest => dest.StatusOperacionalSistemaEnergia, opt => opt.MapFrom(src => src.StatusOperacionalSistemaEnergia))
                .ForMember(dest => dest.DataAtualizacaoEnergiaHoje, opt => opt.MapFrom(src => src.DataAtualizacaoEnergiaHoje))
                .ForMember(dest => dest.DataAtualizacaoCapacidadeTotal, opt => opt.MapFrom(src => src.DataAtualizacaoCapacidadeTotal))
                .ForMember(dest => dest.DataAtualizacaoPotenciaAtual, opt => opt.MapFrom(src => src.DataAtualizacaoPotenciaAtual))
                .ForMember(dest => dest.TipoIntegracao, opt => opt.MapFrom(src => src.TipoIntegracao))
                .AfterMap((src, dest) => dest.DefinirDataCaptura())
                .ReverseMap();
        }
    }
}