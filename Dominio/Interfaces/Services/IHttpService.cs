using Dominio.Dtos;
using HtmlAgilityPack;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Dominio.Interfaces.Services
{
    public interface IHttpService
    {
        Task<HttpServiceDto> GetAsync(string url, Dictionary<string, string>? parametros = null, string? stringContent = null, bool content = false);
        Task<HttpServiceDto> PostAsync(string url, Dictionary<string, string>? parametros = null, string? stringContent = null, bool content = true);
        void RedirecionamentoForcado(bool ativo);
        IDictionary<string, string> ObterCamposOcultos(HtmlNode response, IDictionary<string, string> parametros);
        void InstalaCetificadoDigital(string certificadoPFX, string certificadoSenha);
        void InstalaCetificadoDigital(X509Certificate2 certificate);
        IHttpService ComHeaders(IDictionary<string, string> parametros);
        IHttpService ComCookies(IDictionary<string, string> parametros, string url);
        dynamic? ObterJson(HtmlNode retorno, string? json = null);
        CookieCollection ObterCookies();
        List<Uri> ObterUrls();
        string ObterChavePublicaCertificado();
        X509Certificate2 CriarCertificado(string certPath, string keyPath, string password);
    }
}