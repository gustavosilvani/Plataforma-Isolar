using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public class SungrowAlarmesFalhasService : ISungrowAlarmesFalhasService
    {
        private readonly IHttpService _httpService;
        private readonly ISungrowAutenticacaoService _sungrowAutenticacaoService;
        private readonly IAlertaRepository _alertaRepository;
        private readonly IMapper _mapper;
        private readonly SungrowConfiguracaoIntegracao sungrowConfiguracaoIntegracao = new SungrowConfiguracaoIntegracao();

        public SungrowAlarmesFalhasService(IHttpService httpService, ISungrowAutenticacaoService sungrowAutenticacaoService, IAlertaRepository alertaRepository, IMapper mapper)
        {
            _httpService = httpService;
            _sungrowAutenticacaoService = sungrowAutenticacaoService;
            _alertaRepository = alertaRepository;
            _mapper = mapper;
        }

        #region Metodos Publicos

        public void ExecutaCaptura()
        {
            _ = CapturaAlertas();
        }

        #endregion

        #region Metodos Privados

        private async Task CapturaAlertas()
        {
            string token = await _sungrowAutenticacaoService.Autenticar();
            if (!string.IsNullOrEmpty(token))
            {
                string url = $"{sungrowConfiguracaoIntegracao.UrlBase}getFaultAlarmInfo";
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
                        var tasks = new List<Task>();
                        foreach (var planta in json.result_data.pageList)
                        {
                            if (planta != null)
                            {
                                tasks.Add(ProcessaAlerta(planta.ToString()));
                            }
                        }
                        try
                        {
                            await Task.WhenAll(tasks);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro durante o processamento paralelo: " + ex.Message);
                        }
                        paginaAtual++;
                    }
                    else
                        haMaisPaginas = false;
                }
            }
        }

        private async Task ProcessaAlerta(string alertaJson)
        {
            try
            {
                var alertaDto = JsonConvert.DeserializeObject<SungrowRetornoAlertaPlantaDto>(alertaJson);
                await _alertaRepository.InserirAtualizar(x => x.Codigo == alertaDto.IdPs, _mapper.Map<Alerta>(alertaDto));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar planta: " + ex.Message);
            }
        }

        private Dictionary<string, string> ObterHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "x-access-key", sungrowConfiguracaoIntegracao.AcessKey },
                { "sys_code", "901" }
        };
        }

        private Dictionary<string, string> ObterParametros(string token, string pagina)
        {
            return new Dictionary<string, string>
            {
                { "token", token },
                { "appkey", sungrowConfiguracaoIntegracao.AppKey },
                { "lang", "_pt_BR" },
                { "curPage", pagina },
                { "size", "10" }
            };
        }

        #endregion
    }
}
