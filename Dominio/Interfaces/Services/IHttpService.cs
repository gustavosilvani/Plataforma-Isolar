using Dominio.Dtos;
using HtmlAgilityPack;
using System.Net;

namespace Dominio.Interfaces.Services
{
    public interface IHttpService
    {
        Task<HttpServiceDto> GetAsync(string url, Dictionary<string, string> parametros = null, string stringContent = null, bool content = false);
        Task<HttpServiceDto> PostAsync(string url, Dictionary<string, string> parametros = null, string stringContent = null, bool content = true);
        void RedirecionamentoForcado(bool ativo);
        IDictionary<string, string> ObterCamposOcultos(HtmlNode response, IDictionary<string, string> parametros);
        void InstalaCetificadoDigital(string certificadoPFX, string certificadoSenha);
        IHttpService ComHeaders(IDictionary<string, string> parametros);
        IHttpService ComCookies(IDictionary<string, string> parametros, string url);
        dynamic ObterJson(HtmlNode retorno, string json = null);
        CookieCollection ObterCookies();
        List<Uri> ObterUrls();
        string ObterChavePublicaCertificado();
    }
}