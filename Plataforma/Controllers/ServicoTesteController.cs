using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Infra.CrossCutting.Handlers.Notificacoes;
using Microsoft.AspNetCore.Mvc;

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
            _httpService.InstalaCetificadoDigital(_httpService.CriarCertificado(certPath, keyPath, "Isolar@3025"));

            var url = "http://189.63.223.222:62523";
            var response = await _httpService.GetAsync(url);

            return PResult();
        }

    }
}
