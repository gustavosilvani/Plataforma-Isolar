using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public class SungrowGerenciamentoPlantasService : SungrowServiceBase, ISungrowGerenciamentoPlantasService
    {
        private readonly IPlantaRepository _plantaRepository;
        private readonly IPlantaProducaoRepository _plantaProducaoRepository;

        public SungrowGerenciamentoPlantasService(IHttpService httpService, ISungrowAutenticacaoService autenticacaoService, IPlantaRepository plantaRepository, IPlantaProducaoRepository plantaProducaoRepository, IMapper mapper)
            : base(httpService, autenticacaoService, mapper)
        {
            _plantaRepository = plantaRepository;
            _plantaProducaoRepository = plantaProducaoRepository;
        }

        #region Metodos Publicos

        public async Task ExecutaCapturaAsync()
        {
            await ExecutaCaptura(await AutenticarEPreparar());
        }

        #endregion

        #region Metodos Privados        

        protected override Dictionary<string, string> ObterParametros(string token, string pagina)
        {
            return new Dictionary<string, string>
            {
                { "token", token },
                { "appkey", _configuracaoIntegracao.AppKey },
                { "lang", "_pt_BR" },
                { "curPage", pagina },
                { "size", "10" }
            };
        }

        protected override async Task Processar(string plantaJson)
        {
            try
            {
                SungrowRetornoPlantaListaDto plantaDto = JsonConvert.DeserializeObject<SungrowRetornoPlantaListaDto>(plantaJson);
                var plantaEntidade = await _plantaRepository.InserirAtualizar(x => x.Codigo == plantaDto.IdSistemaEnergia, _mapper.Map<Planta>(plantaDto));

                if (plantaEntidade != null)
                {
                    var plantaProducao = _mapper.Map<PlantaProducao>(plantaDto);
                    plantaProducao.DefinirIdPlanta(plantaEntidade._id);
                    await _plantaProducaoRepository.Inserir(plantaProducao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar planta: " + ex.Message);
            }
        }

        #endregion
    }
}
