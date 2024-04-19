using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public class SungrowGerenciamentoPlantasService : ISungrowGerenciamentoPlantasService
    {
        private readonly IHttpService _httpService;
        private readonly ISungrowAutenticacaoService _sungrowAutenticacaoService;
        private readonly IPlantaRepository _plantaRepository;
        private readonly IPlantaProducaoRepository _plantaProducaoRepository;
        private readonly IMapper _mapper;

        public SungrowGerenciamentoPlantasService(IHttpService httpService, ISungrowAutenticacaoService sungrowAutenticacaoService, IPlantaRepository plantaRepository, IPlantaProducaoRepository plantaProducaoRepository, IMapper mapper)
        {
            _httpService = httpService;
            _sungrowAutenticacaoService = sungrowAutenticacaoService;
            _plantaRepository = plantaRepository;
            _plantaProducaoRepository = plantaProducaoRepository;
            _mapper = mapper;
        }

        #region Metodos Publicos

        public void ExecutaCaptura()
        {
            CapturaPlantas();
        }

        #endregion

        #region Metodos Privados

        private async Task CapturaPlantas()
        {
            string token = await _sungrowAutenticacaoService.Autenticar();

            if (!string.IsNullOrEmpty(token))
            {
                const string url = "https://gateway.isolarcloud.com.hk/openapi/getPowerStationList";
                _httpService.ComHeaders(ObterHeaders());

                bool haMaisPaginas = true;
                int paginaAtual = 1;

                while (haMaisPaginas)
                {
                    var parametros = ObterParametros(token, paginaAtual.ToString());
                    var response = await _httpService.PostAsync(url, null, JsonConvert.SerializeObject(parametros));
                    var json = _httpService.ObterJson(response.DocumentNode);

                    if (json != null && json.result_data.pageList.Count > 0)
                    {

                        foreach (var planta in json.result_data.pageList)
                        {
                            try
                            {
                                SungrowRetornoPlantaListaDto plantaDto = JsonConvert.DeserializeObject<SungrowRetornoPlantaListaDto>(planta.ToString());

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
                                Console.WriteLine("Erro: " + ex.Message);
                            }
                        }
                        paginaAtual++;
                    }
                    else
                        haMaisPaginas = false;

                }
            }
        }


        private Dictionary<string, string> ObterHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "x-access-key", "3ymjkg3buzeit21y5d4smh77ys55jcns" },
                { "sys_code", "901" }
            };
        }

        private Dictionary<string, string> ObterParametros(string token, string pagina)
        {
            return new Dictionary<string, string>
            {
                { "token", token },
                { "appkey", "7EAEAE90F1AB22F3EFF72E1FF044BDCB" },
                { "lang", "_pt_BR" },
                { "curPage", pagina },
                { "size", "10" }
            };
        }

        #endregion
    }
}
