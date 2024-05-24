using Dominio.Entidades;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public class SungrowAutenticacaoService : ISungrowAutenticacaoService
    {
        private readonly IHttpService _httpService;
        private readonly SungrowConfiguracaoIntegracao sungrowConfiguracaoIntegracao = new SungrowConfiguracaoIntegracao();

        public SungrowAutenticacaoService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #region Metodos Publicos

        public async Task<string> Autenticar()
        {
            string url = $"{sungrowConfiguracaoIntegracao.UrlBase}login";

            _httpService.ComHeaders(ObterHeaders());

            var response = await _httpService.PostAsync(url, null, JsonConvert.SerializeObject(ObterParametros()));
            var json = _httpService.ObterJson(response.DocumentNode);

            if (json != null)
                return (string)json.result_data.token;

            return string.Empty;
        }

        #endregion

        #region Metodos Privados

        private Dictionary<string, string> ObterParametros()
        {
            return new Dictionary<string, string>
            {
                { "appkey", sungrowConfiguracaoIntegracao.AppKey },
                { "user_account", "monitoramento@isolarenergy.com.br" },
                { "user_password", "monitoramento@isolar1" },
                { "lang", "_pt_BR" }
            };
        }

        private Dictionary<string, string> ObterHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "x-access-key", sungrowConfiguracaoIntegracao.AcessKey },
                { "sys_code", "sys_code" }
            };
        }

        #endregion


    }
}
