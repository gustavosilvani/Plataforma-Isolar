using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integrações.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integrações.Sungrow
{
    public class SungrowAutenticacaoService : ISungrowAutenticacaoService
    {
        private readonly IHttpService _httpService;

        public SungrowAutenticacaoService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #region Metodos Publicos

        public async Task<string> Autenticar()
        {
            const string url = "https://gateway.isolarcloud.com.hk/openapi/login";

            _httpService.ComHeaders(ObterHeaders());

            var response = await _httpService.PostAsync(url, null, JsonConvert.SerializeObject(ObterParametros()));
            var json = _httpService.ObterJson(response.DocumentNode);

            if (json?.Count > 0)
                return (string)json.result_data.token;

            return string.Empty;
        }

        #endregion

        #region Metodos Privados

        private Dictionary<string, string> ObterParametros()
        {
            return new Dictionary<string, string>
            {
                { "appkey", "7EAEAE90F1AB22F3EFF72E1FF044BDCB" },
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
                { "x-access-key", "3ymjkg3buzeit21y5d4smh77ys55jcns" },
                { "sys_code", "sys_code" }
            };
        }

        #endregion


    }
}
