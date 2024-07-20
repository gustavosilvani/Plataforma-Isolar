using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using System.Text.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public abstract class SungrowServiceBase
    {
        protected readonly IHttpService _httpService;
        protected readonly ISungrowAutenticacaoService _sungrowAutenticacaoService;
        protected readonly IMapper _mapper;
        protected readonly SungrowConfiguracaoIntegracao _configuracaoIntegracao;

        protected SungrowServiceBase(IHttpService httpService, ISungrowAutenticacaoService autenticacaoService, IMapper mapper)
        {
            _httpService = httpService;
            _sungrowAutenticacaoService = autenticacaoService;
            _mapper = mapper;
            _configuracaoIntegracao = new SungrowConfiguracaoIntegracao();
        }

        protected async Task<string> AutenticarEPreparar()
        {
            var token = await _sungrowAutenticacaoService.Autenticar();

            if (!string.IsNullOrEmpty(token))
            {
                _httpService.ComHeaders(ObterHeaders());
            }

            return token;
        }

        private Dictionary<string, string> ObterHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "x-access-key", _configuracaoIntegracao.AcessKey },
                { "sys_code", "901" }
            };
        }

        protected async Task ExecutaCaptura(string token, string url)
        {
            if (string.IsNullOrEmpty(token)) return;

            string baseUrl = _configuracaoIntegracao.UrlBase;
            bool haMaisPaginas = true;
            int paginaAtual = 1;

            while (haMaisPaginas)            {

                var response = await _httpService.PostAsync(url, null, JsonSerializer.Serialize(ObterParametros(token, paginaAtual.ToString())));
                var json = JsonSerializer.Deserialize<dynamic>(response.DocumentNode.InnerText);

                if (json?.result_data?.pageList?.Count > 0)
                {
                    var tasks = new List<Task>();

                    foreach (var item in json.result_data.pageList)
                    {
                        tasks.Add(Processar(item.ToString()));
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

        protected abstract Task Processar(string dado);
        protected abstract Dictionary<string, string> ObterParametros(string token, string pagina);
    }
}