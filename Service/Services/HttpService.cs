using Dominio.Dtos;
using Dominio.Interfaces.Services;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;


namespace Service.Services
{
    public class HttpService : IHttpService
    {
        private HttpClientHandler HttpClientHandler { get; set; }
        private HttpClient Client { get; set; }
        public HtmlDocument HtmlDocument { get; set; }
        private CookieContainer Cookies { get; set; }
        private bool RedirecionamentoManual { get; set; }
        private string ChavePublicaCertificadoDigital { get; set; }

        private List<Uri> Urls { get; set; }

        public HttpService()
        {
            this.Cookies = new CookieContainer();
            this.RedirecionamentoForcado(false);
            this.Urls = new List<Uri>();
            this.HtmlDocument = new HtmlDocument();
        }

        private async Task<HttpServiceDto> RequestAsync(HttpMethod type, string url, Dictionary<string, string>? parametros = null, string? stringContent = null, bool content = false)
        {
            HttpRequestMessage requisicaoParametros = new HttpRequestMessage
            {
                Method = type,
                RequestUri = new Uri(url),
            };

            if (content)
            {
                if (!String.IsNullOrEmpty(stringContent))
                {
                    requisicaoParametros.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");
                }
                else if (parametros != null)
                    requisicaoParametros.Content = new FormUrlEncodedContent(parametros);
            }

            var requisicao = await Client.SendAsync(requisicaoParametros);
            Urls.Add(new Uri(url));

            if (RedirecionamentoManual)
            {
                var redireciona = await ValidaRedirecionamento(requisicao);
                if (redireciona != null)
                    return redireciona;
            }

            var html = await requisicao.Content.ReadAsStringAsync();
            HtmlDocument.LoadHtml(html.Replace("<![CDATA[", "").Replace("]]>", ""));
            return new HttpServiceDto()
            {
                DocumentNode = HtmlDocument.DocumentNode,
                HttpResponseMessage = requisicao
            };
        }

        public async Task<HttpServiceDto> GetAsync(string url, Dictionary<string, string>? parametros = null, string? stringContent = null, bool content = false)
        {
            if (parametros != null)
            {
                url = AdicionaParametrosUrl(url, parametros);
            }

            return await RequestAsync(HttpMethod.Get, url, parametros, stringContent, content);
        }

        public async Task<HttpServiceDto> PostAsync(string url, Dictionary<string, string>? parametros = null, string? stringContent = null, bool content = true)
        {
            return await RequestAsync(HttpMethod.Post, url, parametros, stringContent, content);
        }

        private async Task<HttpServiceDto> ValidaRedirecionamento(HttpResponseMessage requisicao)
        {
            if (requisicao != null && requisicao.StatusCode == HttpStatusCode.Redirect)
            {
                var urlRedirecionamento = requisicao.Headers.Location?.OriginalString;
                if (!string.IsNullOrEmpty(urlRedirecionamento))
                {
                    return await GetAsync(urlRedirecionamento);
                }
            }
            return new HttpServiceDto();
        }


        private string AdicionaParametrosUrl(string url, Dictionary<string, string> parametros)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var item in parametros)
            {
                query[item.Key] = item.Value;
            }

            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }

        public void RedirecionamentoForcado(bool ativo)
        {
            this.RedirecionamentoManual = ativo;
            this.setHttpClientHandler(!ativo);
        }

        private void setHttpClientHandler(bool redirecionamentoAutomatico = true)
        {
            this.HttpClientHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = Cookies,
                AllowAutoRedirect = redirecionamentoAutomatico,
                MaxAutomaticRedirections = 10,
            };
            this.Client = new HttpClient(HttpClientHandler);
            this.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0");
        }

        public IDictionary<string, string> ObterCamposOcultos(HtmlNode response, IDictionary<string, string> parametros)
        {
            var inputsOcultos = response.Descendants("input");

            if (inputsOcultos?.Count() > 0)
                foreach (var item in inputsOcultos)
                {
                    string name = item.Attributes["name"]?.Value ?? item.Attributes["id"]?.Value ?? string.Empty;

                    if (!string.IsNullOrEmpty(name) && item.Attributes["value"]?.Value != null)
                        parametros[name.Replace('–', '-')] = item.Attributes["value"].Value;
                }

            return parametros;
        }


        public void InstalaCetificadoDigital(string certificadoPFX, string certificadoSenha)
        {
            this.RedirecionamentoForcado(false);
            HttpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            HttpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            HttpClientHandler.ClientCertificates.Add(new X509Certificate2(Convert.FromBase64String(certificadoPFX), certificadoSenha));

            this.ChavePublicaCertificadoDigital = Convert.ToBase64String(HttpClientHandler.ClientCertificates[0].GetRawCertData());

            Client = new HttpClient(HttpClientHandler);
            this.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0");
        }

        public IHttpService ComHeaders(IDictionary<string, string> parametros)
        {
            this.Client = new HttpClient(this.HttpClientHandler);
            foreach (var item in parametros)
            {
                this.Client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
            return this;
        }

        public IHttpService ComCookies(IDictionary<string, string> parametros, string url)
        {
            foreach (var item in parametros)
            {
                this.Cookies.Add(new Cookie(item.Key, item.Value) { Domain = new Uri(url).Host });
            }
            return this;
        }

        public dynamic? ObterJson(HtmlNode? retorno, string? json = null)
        {
            if (retorno is not null)
                json = retorno.InnerHtml;

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonConvert.DeserializeObject(json);
        }

        public CookieCollection ObterCookies()
        {
            return this.Cookies.GetAllCookies();
        }

        public List<Uri> ObterUrls()
        {
            return this.Urls;
        }

        public string ObterChavePublicaCertificado()
        {
            return this.ChavePublicaCertificadoDigital;
        }
    }
}