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
        private readonly IPlantaProducaoService _plantaProducaoService;
        private readonly ISungrowAlarmesFalhasService _sungrowAlarmesFalhasService;


        public ServicoTesteController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler, IPlantaProducaoService plantaProducaoService, ISungrowAlarmesFalhasService sungrowAlarmesFalhasService) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
            _plantaProducaoService = plantaProducaoService;
            _sungrowAlarmesFalhasService = sungrowAlarmesFalhasService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {

            _sungrowAlarmesFalhasService.ExecutaCaptura();

            return PResult();
        }

    }
}
