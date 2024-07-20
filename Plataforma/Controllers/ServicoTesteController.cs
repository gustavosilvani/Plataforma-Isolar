using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Infra.CrossCutting.Handlers.Notificacoes;
using Infra.CrossCutting.Helpers;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Integracoes.Sungrow;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plataforma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicoTesteController : AbstractControllerBase
    {
        private readonly IPlantaService _plantaService;
        private readonly IHttpService _httpService;
        private readonly IPlantaProducaoService _plantaProducaoService;
        private readonly ISungrowAlarmesFalhasService _sungrowAlarmesFalhasService;


        public ServicoTesteController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler, IPlantaProducaoService plantaProducaoService, ISungrowAlarmesFalhasService sungrowAlarmesFalhasService, IHttpService httpService) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
            _plantaProducaoService = plantaProducaoService;
            _sungrowAlarmesFalhasService = sungrowAlarmesFalhasService;
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string certPath = @"C:\Workspace\Plataforma-Isolar\certificado.crt";
            string keyPath = @"C:\Workspace\Plataforma-Isolar\chave.key";
            _httpService.CriarCertificado(certPath,keyPath);

            _httpService.GetAsync("187.63.223.222:62523");

            return PResult();
        }

    }
}
