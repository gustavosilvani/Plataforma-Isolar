using HtmlAgilityPack;

namespace Dominio.Dtos
{
    public class HttpServiceDto
    {
        public HtmlNode DocumentNode { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}